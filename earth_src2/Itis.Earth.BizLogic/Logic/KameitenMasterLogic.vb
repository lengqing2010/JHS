Imports Itis.Earth.DataAccess
Imports System.Transactions

Public Class KameitenMasterLogic

    Private kameitenMasterDA As New KameitenMasterDataAccess
    Private commonCheck As New CsvInputCheck
    Private ninsyou As New Ninsyou()
    Private userId As String = Ninsyou.GetUserID()
    Private InputDate As String

    'CSV�A�b�v���[�h�������
    Private CsvInputMaxLineCount As String = CStr(System.Configuration.ConfigurationSettings.AppSettings("CsvInputMaxLineCount"))

    ''' <summary>�A�b�v���[�h�Ǘ����擾����</summary>
    ''' <returns>�A�b�v���[�h�Ǘ��f�[�^�e�[�u��</returns>
    Public Function GetInputKanri() As Data.DataTable

        Return kameitenMasterDA.SelInputKanri()

    End Function

    ''' <summary>�A�b�v���[�h�Ǘ��̌������擾����</summary>
    ''' <returns>�A�b�v���[�h�Ǘ��̌����f�[�^�e�[�u��</returns>
    Public Function GetInputKanriCount() As Integer

        Return kameitenMasterDA.SelInputKanriCount()

    End Function

    ''' <summary>�����X�R�[�h���擾����</summary>
    ''' <returns>�����X�R�[�h</returns>
    Public Function GetKameitenCd(ByVal strKameitenCd As String, ByVal strKbn As String) As Boolean

        Return kameitenMasterDA.SelKameitenCd(strKameitenCd, strKbn)

    End Function

    ''' <summary>CSV�t�@�C�����`�F�b�N����</summary>
    ''' <returns>CSV�t�@�C�����`�F�b�N����</returns>
    Public Function ChkCsvFile(ByRef strUmuFlg As String, ByVal hidInsLineNo As String, ByVal arrCsvLine() As String, ByVal strFileMei As String) As String

        'Dim myStream As IO.Stream                           '���o�̓X�g���[��
        'Dim myReader As IO.StreamReader                     '�X�g���[�����[�_�[
        Dim strReadLine As String                           '�捞�t�@�C���Ǎ��ݍs
        Dim intLineCount As Integer = 0                     '���C����
        Dim strCsvLine() As String                          'CSV�t�@�C�����e
        '  Dim strFileMei As String                            'CSV�t�@�C����
        Dim strEdiJouhouSakuseiDate As String = String.Empty 'EDI���쐬��
        Dim dtOk As New Data.DataTable                      '�����X�}�X�^
        Dim dtError As New Data.DataTable                   '�����X�G���[
        Dim dtInsKameiten As New Data.DataTable             ' �V�K�o�^�ƂȂ�����X���ނ��d�����ĂȂ��ꍇ�A�o�^�����𑱂���
        Dim strMsg As String = ""
        '�����X���i�������@���ʑΉ��}�X�^���쐬
        CreateOkDataTable(dtOk)
        CreateOkDataTable(dtInsKameiten)
        '�����X���i�������@���ʑΉ��G���[�e�[�u�����쐬
        CreateErrorDataTable(dtError)

        '=========2012/05/31 �ԗ� 407553�̑Ή� �ǉ���===================================
        Dim dtError1 As New Data.DataTable
        CreateErrorDataTable(dtError1)

        '=========2012/05/31 �ԗ� 407553�̑Ή� �ǉ���===================================
        Try
            'If arrCsvLine Is Nothing Then
            '    '�V�X�e��Date
            InputDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")

            '    'CSV�t�@�C����
            'strFileMei = strFileMei

            '    '���o�̓X�g���[��
            '    myStream = fupId.FileContent

            '    '�X�g���[�����[�_�[
            '    myReader = New IO.StreamReader(myStream, System.Text.Encoding.GetEncoding(932))


            '    Do
            '        '�捞�t�@�C����ǂݍ���
            '        ReDim Preserve strCsvLine(intLineCount)
            '        strCsvLine(intLineCount) = myReader.ReadLine()
            '        intLineCount += 1
            '    Loop Until myReader.EndOfStream

            '    'CSV�A�b�v���[�h��������`�F�b�N
            '    For i As Integer = strCsvLine.Length - 1 To 0 Step -1
            '        If Not strCsvLine(i).Trim.Equals(String.Empty) Then
            '            If i > CsvInputMaxLineCount Then
            '                Return String.Format(Messages.Instance.MSG040E, CStr(CsvInputMaxLineCount))
            '            ElseIf i < 1 Then
            '                Return String.Format(Messages.Instance.MSG048E)
            '            Else
            '                Exit For
            '            End If
            '        End If
            '    Next
            'Else
            strCsvLine = arrCsvLine
            'End If
            'EDI���쐬��
            strEdiJouhouSakuseiDate = Right("0" & strCsvLine(1).Split(",")(0).Trim, 10)

            'CSV�t�@�C�����`�F�b�N
            For i As Integer = 1 To strCsvLine.Length - 1
                strReadLine = strCsvLine(i)
                If (Not strReadLine Is Nothing) AndAlso (Replace(strReadLine, ",", "") <> "") Then

                    If strReadLine.Split(",")(0).Length < 10 Then
                        strReadLine = "0" & strReadLine
                    End If

                    '�J���}��ǉ�
                    If strReadLine.Split(",").Length < CsvInputCheck.KAMEITEN_FIELD_COUNT Then
                        strReadLine = strReadLine & StrDup(CsvInputCheck.KAMEITEN_FIELD_COUNT - strReadLine.Split(",").Length, ",")
                    End If

                    '�t�B�[���h���`�F�b�N
                    If Not commonCheck.ChkFieldCount(strReadLine.Split(",").Length, CsvInputCheck.KAMEITEN) Then
                        '�G���[�f�[�^�̏���
                        Call Me.ErrorLineSyori(i + 1, strReadLine, dtError)
                        Continue For
                    End If

                    strReadLine = SetUmekusa(strReadLine)

                    '���݃`�F�b�N
                    If Not Me.ChkSonZai(strReadLine, strMsg) Then
                        '�G���[�f�[�^�̏���
                        Call Me.ErrorLineSyori(i + 1, strReadLine, dtError)
                        Continue For
                    End If

                    '���ڍő咷�`�F�b�N
                    If Not commonCheck.ChkMaxLength(strReadLine, CsvInputCheck.KAMEITEN) Then
                        '�G���[�f�[�^�̏���
                        Call Me.ErrorLineSyori(i + 1, strReadLine, dtError)
                        Continue For
                    End If

                    '�֑������`�F�b�N
                    If Not commonCheck.ChkKinjiMoji(strReadLine) Then
                        '�G���[�f�[�^�̏���
                        Call Me.ErrorLineSyori(i + 1, strReadLine, dtError)
                        Continue For
                    End If

                    '���l�^���ڃ`�F�b�N
                    If Not commonCheck.ChkSuuti(strReadLine, CsvInputCheck.KAMEITEN) Then
                        '�G���[�f�[�^�̏���
                        Call Me.ErrorLineSyori(i + 1, strReadLine, dtError)
                        Continue For
                    End If


                    If Not commonCheck.ChkNotDate(strReadLine, CsvInputCheck.KAMEITEN) Then
                        '�G���[�f�[�^�̏���
                        Call Me.ErrorLineSyori(i + 1, strReadLine, dtError)
                        Continue For
                    End If
                    '�K�{�`�F�b�N
                    If Not commonCheck.ChkNotNull(strReadLine, CsvInputCheck.KAMEITEN) Then
                        '�G���[�f�[�^�̏���
                        Call Me.ErrorLineSyori(i + 1, strReadLine, dtError)
                        Continue For
                    End If

                    '===========2012/05/08 �ԗ� 407553�̑Ή� �ǉ���============================
                    '�X�V�����`�F�b�N()
                    If Not Me.ChkUpdDate(strReadLine) Then
                        '�G���[�f�[�^�̏���
                        Call Me.ErrorLineSyori(i + 1, strReadLine, dtError)
                        Continue For
                    End If
                    '===========2012/05/08 �ԗ� 407553�̑Ή� �ǉ���============================

                    '===========2012/05/23 �ԗ� 407553�̑Ή� �ǉ���============================
                    '�����X�R�[�h
                    Dim strKameitenCd As String = strReadLine.Split(",")(2).Trim
                    '�����X�̍X�V����
                    Dim strKameitenUpdDate As String = strReadLine.Split(",")(91).Trim
                    '�����X�}�X�^�̍X�V����
                    Dim dtMstUpdDate As New Data.DataTable
                    dtMstUpdDate = kameitenMasterDA.SelKameitenUpdDate(strKameitenCd)
                    Dim strKameitenMstUpdDate As String

                    If dtMstUpdDate.Rows.Count > 0 Then
                        strKameitenMstUpdDate = kameitenMasterDA.SelKameitenUpdDate(strKameitenCd).Rows(0).Item(0).ToString.Trim
                    Else
                        strKameitenMstUpdDate = String.Empty
                    End If

                    If (Not strKameitenUpdDate.Equals(String.Empty)) OrElse _
                        ((strKameitenUpdDate.Equals(String.Empty)) AndAlso (strKameitenMstUpdDate.Equals(String.Empty))) Then
                        '===========2012/05/23 �ԗ� 407553�̑Ή� �ǉ���============================
                        If hidInsLineNo.IndexOf(i & ",") <> -1 Then
                            '���i�f�[�^�̏���(insert)
                            If hidInsLineNo.IndexOf(i & ",") = 0 Then
                                Call Me.OkLineSyori(strReadLine, dtInsKameiten)
                            Else
                                If hidInsLineNo.Substring((hidInsLineNo.IndexOf(i & ",") - 1), 1) = "," Then
                                    Call Me.OkLineSyori(strReadLine, dtInsKameiten)
                                Else
                                    Call Me.OkLineSyori(strReadLine, dtOk)
                                End If
                            End If
                        Else
                            '���i�f�[�^�̏���(update)
                            Call Me.OkLineSyori(strReadLine, dtOk)
                        End If




                        '===========2012/05/23 �ԗ� 407553�̑Ή� �ǉ���============================
                    Else
                        Call Me.ErrorLineSyori(i + 1, strReadLine, dtError1)
                        Continue For
                    End If
                    '===========2012/05/23 �ԗ� 407553�̑Ή� �ǉ���============================

                    'For intLoopI As Integer = 0 To hidInsLineNo.Split(",").Length - 1
                    '    If i = hidInsLineNo.Split(",")(intLoopI) Then
                    '        Me.SetOkDataRow(strReadLine, dtInsKameiten, "1")
                    '        Exit For
                    '    End If
                    'Next



                End If
            Next

            '===========2012/05/31 �ԗ� 407553�̑Ή� �ǉ���============================
            If dtError1.Rows.Count > 0 Then
                dtOk.Rows.Clear()
                dtInsKameiten.Rows.Clear()

                If Not CsvFileInput(dtOk, dtError1, InputDate, userId, strFileMei, strEdiJouhouSakuseiDate, dtInsKameiten) Then
                    Return Messages.Instance.MSG050E
                Else
                    Return Messages.Instance.MSG2073E
                End If

            End If
            '===========2012/05/31 �ԗ� 407553�̑Ή� �ǉ���============================

            '�G���[�L���t���O��ݒ肷��
            If dtError.Rows.Count > 0 Then
                strUmuFlg = "1"
            End If

            'CSV�t�@�C�����捞



            If Not CsvFileInput(dtOk, dtError, InputDate, userId, strFileMei, strEdiJouhouSakuseiDate, dtInsKameiten) Then
                If strMsg <> "" Then
                    Return strMsg
                Else
                    Return Messages.Instance.MSG050E
                End If

            Else
                Return strMsg
            End If

        Catch ex As Exception
            Return Messages.Instance.MSG049E
        End Try

    End Function

    ''' <summary>
    ''' �����X�R�[�h��"0"�Ŗ��߂�
    ''' </summary>
    Public Function SetUmekusa(ByVal StrLine As String) As String

        Dim arrLine() As String = StrLine.Split(",")
        '�����X���ށi�� �O0����5���ϊ��j
        If Not String.IsNullOrEmpty(arrLine(2).ToString) Then
            arrLine(2) = Right("00000" & arrLine(2).Trim, 5)
        End If

        '�n���ށi�� �O0����4���ϊ��j
        If Not String.IsNullOrEmpty(arrLine(11).ToString) Then
            If arrLine(1).Trim.ToUpper = "K" Then
                arrLine(11) = Right("00000" & arrLine(11).Trim, 5)
            Else
                arrLine(11) = Right("0000" & arrLine(11).Trim, 4)
            End If

        End If

        '�c�Ə����ށi�� �O0����4���ϊ��j
        If Not String.IsNullOrEmpty(arrLine(13).ToString) Then
            arrLine(13) = Right("0000" & arrLine(13).Trim, 4)
        End If

        '���敪1�A2�A3���i���ށi�� �O0����5���ϊ��j
        If Not String.IsNullOrEmpty(arrLine(30).ToString) Then
            arrLine(30) = Right("00000" & arrLine(30).Trim, 5)
        End If
        If Not String.IsNullOrEmpty(arrLine(32).ToString) Then
            arrLine(32) = Right("00000" & arrLine(32).Trim, 5)
        End If
        If Not String.IsNullOrEmpty(arrLine(34).ToString) Then
            arrLine(34) = Right("00000" & arrLine(34).Trim, 5)
        End If

        '�ǉ�_���l��ʇ@�`�D�i�� �O0����2���ϊ��j
        If Not String.IsNullOrEmpty(arrLine(96).ToString) Then
            arrLine(96) = Right("00" & arrLine(96).Trim, 2)
        End If
        If Not String.IsNullOrEmpty(arrLine(98).ToString) Then
            arrLine(98) = Right("00" & arrLine(98).Trim, 2)
        End If
        If Not String.IsNullOrEmpty(arrLine(100).ToString) Then
            arrLine(100) = Right("00" & arrLine(100).Trim, 2)
        End If
        If Not String.IsNullOrEmpty(arrLine(102).ToString) Then
            arrLine(102) = Right("00" & arrLine(102).Trim, 2)
        End If
        If Not String.IsNullOrEmpty(arrLine(104).ToString) Then
            arrLine(104) = Right("00" & arrLine(104).Trim, 2)
        End If


        '(������������ �敪0�̏ꍇ:�O0����5���ϊ� �敪1�̏ꍇ:�O0����4���ϊ�)
        If Not String.IsNullOrEmpty(arrLine(37).ToString) Then
            If "0".Equals(arrLine(36).ToString) Then
                arrLine(37) = Right("00000" & arrLine(37).Trim, 5)
            End If
            If "1".Equals(arrLine(36).ToString) Then
                arrLine(37) = Right("0000" & arrLine(37).Trim, 4)
            End If

        End If

        '����������}�ԁ� �O0����2���ϊ��j
        If Not String.IsNullOrEmpty(arrLine(38).ToString) Then
            arrLine(38) = Right("00" & arrLine(38).Trim, 2)
        End If

        '�H��������R�[�h(���H�������� �敪0�̏ꍇ:�O0����5���ϊ��敪1�̏ꍇ:�O0����4���ϊ�)
        If Not String.IsNullOrEmpty(arrLine(41).ToString) Then
            If "0".Equals(arrLine(40).ToString) Then
                arrLine(41) = Right("00000" & arrLine(41).Trim, 5)
            End If
            If "1".Equals(arrLine(40).ToString) Then
                arrLine(41) = Right("0000" & arrLine(41).Trim, 4)
            End If
        End If
        '�H��������}�ԁi�� �O0����2���ϊ��j
        If Not String.IsNullOrEmpty(arrLine(42).ToString) Then
            arrLine(42) = Right("00" & arrLine(42).Trim, 2)
        End If

        ' �̑��i������R�[�h(���̑��i������ �敪0�̏ꍇ:�O0����5���ϊ� �敪1�̏ꍇ:�O0����4���ϊ�)
        If Not String.IsNullOrEmpty(arrLine(45).ToString) Then
            If "0".Equals(arrLine(44).ToString) Then
                arrLine(45) = Right("00000" & arrLine(45).Trim, 5)
            End If
            If "1".Equals(arrLine(44).ToString) Then
                arrLine(45) = Right("0000" & arrLine(45).Trim, 4)
            End If
        End If
        '�̑��i������}�ԁi�� �O0����2���ϊ��j
        If Not String.IsNullOrEmpty(arrLine(46).ToString) Then
            arrLine(46) = Right("00" & arrLine(46).Trim, 2)
        End If

        '��������������R�[�h(���������������� �敪0�̏ꍇ:�O0����5���ϊ� �敪1�̏ꍇ:�O0����4���ϊ�)
        If Not String.IsNullOrEmpty(arrLine(49).ToString) Then
            If "0".Equals(arrLine(48).ToString) Then
                arrLine(49) = Right("00000" & arrLine(49).Trim, 5)
            End If
            If "1".Equals(arrLine(48).ToString) Then
                arrLine(49) = Right("0000" & arrLine(49).Trim, 4)
            End If
        End If
        '��������������}�ԁi�� �O0����2���ϊ��j
        If Not String.IsNullOrEmpty(arrLine(50).ToString) Then
            arrLine(50) = Right("00" & arrLine(50).Trim, 2)
        End If

        '������R�[�h5(��������敪5���@0�̏ꍇ:�O0����5���ϊ� 1�̏ꍇ:�O0����4���ϊ�)
        If Not String.IsNullOrEmpty(arrLine(53).ToString) Then
            If "0".Equals(arrLine(52).ToString) Then
                arrLine(53) = Right("00000" & arrLine(53).Trim, 5)
            End If
            If "1".Equals(arrLine(52).ToString) Then
                arrLine(53) = Right("0000" & arrLine(53).Trim, 4)
            End If
        End If
        '������}��5�� �O0����2���ϊ��j
        If Not String.IsNullOrEmpty(arrLine(54).ToString) Then
            arrLine(54) = Right("00" & arrLine(54).Trim, 2)
        End If

        '������R�[�h6(��������敪6���@0�̏ꍇ:�O0����5���ϊ� 1�̏ꍇ:�O0����4���ϊ�)
        If Not String.IsNullOrEmpty(arrLine(57).ToString) Then
            If "0".Equals(arrLine(56).ToString) Then
                arrLine(57) = Right("00000" & arrLine(57).Trim, 5)
            End If
            If "1".Equals(arrLine(56).ToString) Then
                arrLine(57) = Right("0000" & arrLine(57).Trim, 4)
            End If
        End If
        '������}��6�� �O0����2���ϊ��j
        If Not String.IsNullOrEmpty(arrLine(58).ToString) Then
            arrLine(58) = Right("00" & arrLine(58).Trim, 2)
        End If

        '������R�[�h7(��������敪7���@0�̏ꍇ:�O0����5���ϊ� 1�̏ꍇ:�O0����4���ϊ�)
        If Not String.IsNullOrEmpty(arrLine(61).ToString) Then
            If "0".Equals(arrLine(60).ToString) Then
                arrLine(61) = Right("00000" & arrLine(61).Trim, 5)
            End If
            If "1".Equals(arrLine(60).ToString) Then
                arrLine(61) = Right("0000" & arrLine(61).Trim, 4)
            End If
        End If
        '������}��7�� �O0����2���ϊ��j
        If Not String.IsNullOrEmpty(arrLine(62).ToString) Then
            arrLine(62) = Right("00" & arrLine(62).Trim, 2)
        End If

        '�s���{�����ށi���O0����2���ϊ��j
        If Not String.IsNullOrEmpty(arrLine(17).ToString) Then
            arrLine(17) = Right("00" & arrLine(17).Trim, 2)
        End If

        '����ްNO�i���O0����5���ϊ��j
        '================2012/04/06 �ԗ� �v�]�̑Ή� �C����============================
        'If Not String.IsNullOrEmpty(arrLine(9).ToString) Then
        '    arrLine(9) = Right("00000" & arrLine(9).Trim, 5)
        'End If
        If Not String.IsNullOrEmpty(arrLine(9).ToString) Then
            If (arrLine(9).Trim.Length < 5) AndAlso commonCheck.CheckHankaku(arrLine(9).Trim) Then
                arrLine(9) = Right("00000" & arrLine(9).Trim, 5)
            End If
        End If
        '================2012/04/06 �ԗ� �v�]�̑Ή� �C����============================

        '�X�֔ԍ���***-****�@�O����***������3�����Ă��Ȃ��ꍇ�@�O��0����3���ϊ�
        If Not String.IsNullOrEmpty(arrLine(70).ToString) Then
            arrLine(70) = Right("000" & arrLine(70).Trim, 8)
        End If

        '�d�b�ԍ����擪�̕�����0�ȊO�̐����̏ꍇ�擪��0��������
        If Not String.IsNullOrEmpty(arrLine(77).ToString) AndAlso Not "0".Equals(Left(arrLine(77).Trim, 1)) Then
            arrLine(77) = "0" & arrLine(77).Trim
        End If

        'FAX�ԍ����擪�̕�����0�ȊO�̐����̏ꍇ�擪��0��������
        If Not String.IsNullOrEmpty(arrLine(78).ToString) AndAlso Not "0".Equals(Left(arrLine(78).Trim, 1)) Then
            arrLine(78) = "0" & arrLine(78).Trim
        End If

        Return String.Join(",", arrLine)

    End Function
    ''' <summary>CSV�t�@�C�����捞����</summary>
    ''' <returns>CSV�t�@�C�����捞����</returns>
    Public Function CsvFileInput(ByVal dtOk As Data.DataTable, ByVal dtError As Data.DataTable, ByVal InputDate As String, ByVal userId As String, ByVal strFileMei As String, ByVal strEdiJouhouSakuseiDate As String, ByVal dtInsKameiten As Data.DataTable) As Boolean

        Using scope As New TransactionScope(TransactionScopeOption.RequiresNew, New System.TimeSpan(0, 10, 0))
            Try
                '�����X�������@���ʑΉ��}�X�^��o�^()
                If dtOk.Rows.Count > 0 OrElse dtInsKameiten.Rows.Count > 0 Then
                    If Not SuccessSyori(dtOk, dtInsKameiten) Then
                        Throw New ApplicationException
                    End If

                End If

                '�����X���ꊇ�捞�G���[���e�[�u����o�^()
                If dtError.Rows.Count > 0 Then
                    If Not kameitenMasterDA.InstKameitenInfoIttukatuError(dtError) Then
                        Throw New ApplicationException
                    End If
                End If

                '�A�b�v���[�h�Ǘ��e�[�u����o�^()
                If Not kameitenMasterDA.InsInputKanri(InputDate, strFileMei, strEdiJouhouSakuseiDate, CInt(IIf(dtError.Rows.Count, 1, 0)), userId) Then
                    Throw New ApplicationException
                End If

                scope.Complete()

            Catch ex As Exception
                scope.Dispose()
                Return False
            End Try
        End Using

        Return True

    End Function
    Private Sub AddMsg(ByRef strMsg As String, ByVal strErr As String)
        If strMsg = "" Then
            strMsg = strErr
        Else
            strMsg = strMsg & "\r\n" & strErr
        End If
    End Sub
    Private Sub CHKSeikyuu(ByRef intFieldCount As String, ByVal intSeikyuu As Integer, ByRef strMsg As String, ByVal strErrMsg As String, ByRef successFlg As Boolean)
        Dim strMei As String = ""
        Dim arrLine2() As String = Split(intFieldCount, ",")

        '����������敪�E���������溰�ށE����������}��
        If intFieldCount.Split(",")(intSeikyuu).Trim = "0" Then
            If Not kameitenMasterDA.SelKameitenCd(intFieldCount.Split(",")(intSeikyuu + 1).Trim, , strMei) Then
                AddMsg(strMsg, String.Format(Messages.Instance.MSG053E, _
                intFieldCount.Split(",")(intSeikyuu).Trim & "�E" & intFieldCount.Split(",")(intSeikyuu + 1).Trim & "�E" & intFieldCount.Split(",")(intSeikyuu + 2).Trim, _
                "�����X", strErrMsg))
                successFlg = False
            Else
                If arrLine2(intSeikyuu + 3) = "" Then
                    arrLine2(intSeikyuu + 3) = strMei
                End If
            End If
        End If
        If intFieldCount.Split(",")(intSeikyuu).Trim = "1" Then
            If Not kameitenMasterDA.SelTyousakaisya(intFieldCount.Split(",")(intSeikyuu + 1).Trim, intFieldCount.Split(",")(intSeikyuu + 2).Trim, strMei) Then
                AddMsg(strMsg, String.Format(Messages.Instance.MSG053E, _
                intFieldCount.Split(",")(intSeikyuu).Trim & "�E" & intFieldCount.Split(",")(intSeikyuu + 1).Trim & "�E" & intFieldCount.Split(",")(intSeikyuu + 2).Trim, _
                "�������", strErrMsg))
                successFlg = False
            Else
                If arrLine2(intSeikyuu + 3) = "" Then
                    arrLine2(intSeikyuu + 3) = strMei
                End If
            End If
        End If
        intFieldCount = String.Join(",", arrLine2)
    End Sub
    ''' <summary>���݃`�F�b�N</summary>
    Private Function ChkSonZai(ByRef intFieldCount As String, ByRef strMsg As String) As Boolean
        Dim arrLine2() As String = Split(intFieldCount, ",")
        Dim strMei As String = ""

        Dim successFlg As Boolean = True


        '����ްNO
        If Not String.IsNullOrEmpty(intFieldCount.Split(",")(9).Trim) Then
            If Not kameitenMasterDA.SelKameitenCd(intFieldCount.Split(",")(9).Trim, strMei) Then
                '=================2012/03/09 �ԗ� �G���[�̑Ή� �폜��===========================
                'Return False
                '=================2012/03/09 �ԗ� �G���[�̑Ή� �폜��===========================
            Else
                If arrLine2(10).Trim = "" Then
                    arrLine2(10) = strMei
                End If
            End If
        End If

        If Not String.IsNullOrEmpty(intFieldCount.Split(",")(17).Trim) Then
            If Not kameitenMasterDA.SelTodoufuken(intFieldCount.Split(",")(17).Trim, strMei) Then

                Return False
            Else
                If arrLine2(18).Trim = "" Then
                    arrLine2(18) = strMei
                End If
            End If
        End If

        '�n����
        If Not String.IsNullOrEmpty(intFieldCount.Split(",")(11).Trim) Then
            If Not kameitenMasterDA.SelKeiretu(intFieldCount.Split(",")(11).Trim, intFieldCount.Split(",")(1).Trim, strMei) Then
                AddMsg(strMsg, String.Format(Messages.Instance.MSG053E, intFieldCount.Split(",")(11).Trim, "�n��", "�n����"))
                successFlg = False
            Else
                If arrLine2(12).Trim = "" Then
                    arrLine2(12) = strMei
                End If
            End If
        End If

        '�c�Ə�����
        If Not String.IsNullOrEmpty(intFieldCount.Split(",")(13).Trim) Then
            If Not kameitenMasterDA.SelEigyousyo(intFieldCount.Split(",")(13).Trim, strMei) Then
                AddMsg(strMsg, String.Format(Messages.Instance.MSG053E, intFieldCount.Split(",")(13).Trim, "�c�Ə�", "�c�Ə�����"))
                successFlg = False
            Else
                If arrLine2(14).Trim = "" Then
                    arrLine2(14) = strMei
                End If
            End If
        End If
        '�c�ƒS����
        If Not String.IsNullOrEmpty(intFieldCount.Split(",")(22).Trim) Then
            If Not kameitenMasterDA.SelAccount(intFieldCount.Split(",")(22).Trim, strMei) Then
                AddMsg(strMsg, String.Format(Messages.Instance.MSG053E, intFieldCount.Split(",")(22).Trim, "�n�ՔF��", "�c�ƒS����"))
                successFlg = False
            Else
                If arrLine2(23).Trim = "" Then
                    arrLine2(23) = strMei
                End If
            End If
        End If

        '���c�ƒS����
        If Not String.IsNullOrEmpty(intFieldCount.Split(",")(24).Trim) Then
            If Not kameitenMasterDA.SelAccount(intFieldCount.Split(",")(24).Trim, strMei) Then
                '============2012/06/01 �ԗ� 407553�̑Ή� �폜��===========================
                'AddMsg(strMsg, String.Format(Messages.Instance.MSG053E, intFieldCount.Split(",")(24).Trim, "�n�ՔF��", "���c�ƒS����"))
                'successFlg = False
                '============2012/06/01 �ԗ� 407553�̑Ή� �폜��===========================
            Else
                If arrLine2(25).Trim = "" Then
                    arrLine2(25) = strMei
                End If
            End If
        End If

        '���敪1�A2�A3���i�R�[�h
        If Not String.IsNullOrEmpty(intFieldCount.Split(",")(30).Trim) Then
            If Not kameitenMasterDA.SelSyouhinCd(intFieldCount.Split(",")(30).Trim, strMei) Then
                AddMsg(strMsg, String.Format(Messages.Instance.MSG053E, intFieldCount.Split(",")(30).Trim, "���i", "���敪1"))
                successFlg = False
            Else
                If arrLine2(31).Trim = "" Then
                    arrLine2(31) = strMei
                End If
            End If
        End If
        If Not String.IsNullOrEmpty(intFieldCount.Split(",")(32).Trim) Then
            If Not kameitenMasterDA.SelSyouhinCd(intFieldCount.Split(",")(32).Trim, strMei) Then
                AddMsg(strMsg, String.Format(Messages.Instance.MSG053E, intFieldCount.Split(",")(32).Trim, "���i", "���敪2"))
                successFlg = False
            Else
                If arrLine2(33).Trim = "" Then
                    arrLine2(33) = strMei
                End If
            End If
        End If
        If Not String.IsNullOrEmpty(intFieldCount.Split(",")(34).Trim) Then
            If Not kameitenMasterDA.SelSyouhinCd(intFieldCount.Split(",")(34).Trim, strMei) Then
                AddMsg(strMsg, String.Format(Messages.Instance.MSG053E, intFieldCount.Split(",")(34).Trim, "���i", "���敪3"))
                successFlg = False
            Else
                If arrLine2(35).Trim = "" Then
                    arrLine2(35) = strMei
                End If
            End If
        End If

        '==================2012/03/13 �ԗ� �ǉ���====================================
        '����۰��̧��.���敪1�A2�A3���i���ށi�� �O0����5���ϊ��j�ɓ��͂�����ہA
        '���̺��ނ����iϽ�.souko_cd = "115'�@�ɑ��݂��Ȃ��@�������́@���̺��ނ�'00000' �ł͂Ȃ��ꍇ�́A�G���[�ł��B

        '���敪n���i����
        Dim strSyouhinCd As String

        '���敪1
        strSyouhinCd = intFieldCount.Split(",")(30).Trim
        If Not String.IsNullOrEmpty(strSyouhinCd) Then
            If (Not strSyouhinCd.Equals("00000")) AndAlso (Not kameitenMasterDA.SelSoukoCheck(strSyouhinCd)) Then
                AddMsg(strMsg, String.Format(Messages.Instance.MSG055E, strSyouhinCd.Trim, "���敪1"))
                successFlg = False
            End If
        End If
        '���敪2
        strSyouhinCd = intFieldCount.Split(",")(32).Trim
        If Not String.IsNullOrEmpty(strSyouhinCd) Then
            If (Not strSyouhinCd.Equals("00000")) AndAlso (Not kameitenMasterDA.SelSoukoCheck(strSyouhinCd)) Then
                AddMsg(strMsg, String.Format(Messages.Instance.MSG055E, strSyouhinCd, "���敪2"))
                successFlg = False
            End If
        End If
        '���敪3
        strSyouhinCd = intFieldCount.Split(",")(34).Trim
        If Not String.IsNullOrEmpty(strSyouhinCd) Then
            If (Not strSyouhinCd.Equals("00000")) AndAlso (Not kameitenMasterDA.SelSoukoCheck(strSyouhinCd)) Then
                AddMsg(strMsg, String.Format(Messages.Instance.MSG055E, strSyouhinCd, "���敪3"))
                successFlg = False
            End If
        End If
        '==================2012/03/13 �ԗ� �ǉ���====================================

        intFieldCount = String.Join(",", arrLine2)

        CHKSeikyuu(intFieldCount, 36, strMsg, "����������敪�E���������溰�ށE����������}��", successFlg)

        CHKSeikyuu(intFieldCount, 40, strMsg, "�H��������敪�E�H�������溰�ށE�H��������}��", successFlg)

        CHKSeikyuu(intFieldCount, 44, strMsg, "�̑��i������敪�E�̑��i�����溰�ށE�̑��i������}��", successFlg)

        CHKSeikyuu(intFieldCount, 48, strMsg, "��������������敪�E�������������溰�ށE��������������}��", successFlg)

        CHKSeikyuu(intFieldCount, 52, strMsg, "������敪5�E�����溰��5�E������}��5", successFlg)

        CHKSeikyuu(intFieldCount, 56, strMsg, "������敪6�E�����溰��6�E������}��6", successFlg)

        CHKSeikyuu(intFieldCount, 60, strMsg, "������敪7�E�����溰��7�E������}��7", successFlg)
        arrLine2 = Split(intFieldCount, ",")

        '==========��2013/03/08 �ԗ� 407584 �ǉ���======================
        '���i�R�[�h(����۰��̧��.���i�R�[�h�@���@���iϽ�.�q�ɺ��� =�@200 �ȊO�̏ꍇ)
        If Not String.IsNullOrEmpty(arrLine2(84).Trim) Then
            If Not kameitenMasterDA.SelSyouhinSoukoCdCheck(arrLine2(84).Trim, "200") Then
                AddMsg(strMsg, String.Format(Messages.Instance.MSG2075E, arrLine2(84).Trim))
                successFlg = False
            End If
        End If
        '==========��2013/03/08 �ԗ� 407584 �ǉ���======================

        '�ǉ�_���l���
        If Not String.IsNullOrEmpty(intFieldCount.Split(",")(96).Trim) Then
            If Not kameitenMasterDA.SelMeisyouCd(intFieldCount.Split(",")(96).Trim, strMei) Then
                AddMsg(strMsg, String.Format(Messages.Instance.MSG053E, intFieldCount.Split(",")(96).Trim, "����", "�ǉ�_���l��ʇ@"))
                successFlg = False
            Else
                If arrLine2(97).Trim = "" Then
                    arrLine2(97) = strMei
                End If
            End If
        End If
        If Not String.IsNullOrEmpty(intFieldCount.Split(",")(98).Trim) Then
            If Not kameitenMasterDA.SelMeisyouCd(intFieldCount.Split(",")(98).Trim, strMei) Then
                AddMsg(strMsg, String.Format(Messages.Instance.MSG053E, intFieldCount.Split(",")(98).Trim, "����", "�ǉ�_���l��ʇA"))
                successFlg = False
            Else
                If arrLine2(99).Trim = "" Then
                    arrLine2(99) = strMei
                End If
            End If
        End If
        If Not String.IsNullOrEmpty(intFieldCount.Split(",")(100).Trim) Then
            If Not kameitenMasterDA.SelMeisyouCd(intFieldCount.Split(",")(100).Trim, strMei) Then
                AddMsg(strMsg, String.Format(Messages.Instance.MSG053E, intFieldCount.Split(",")(100).Trim, "����", "�ǉ�_���l��ʇB"))
                successFlg = False
            Else
                If arrLine2(101).Trim = "" Then
                    arrLine2(101) = strMei
                End If
            End If
        End If
        If Not String.IsNullOrEmpty(intFieldCount.Split(",")(102).Trim) Then
            If Not kameitenMasterDA.SelMeisyouCd(intFieldCount.Split(",")(102).Trim, strMei) Then
                AddMsg(strMsg, String.Format(Messages.Instance.MSG053E, intFieldCount.Split(",")(102).Trim, "����", "�ǉ�_���l��ʇC"))
                successFlg = False
            Else
                If arrLine2(103).Trim = "" Then
                    arrLine2(103) = strMei
                End If
            End If
        End If
        If Not String.IsNullOrEmpty(intFieldCount.Split(",")(104).Trim) Then
            If Not kameitenMasterDA.SelMeisyouCd(intFieldCount.Split(",")(104).Trim, strMei) Then
                AddMsg(strMsg, String.Format(Messages.Instance.MSG053E, intFieldCount.Split(",")(104).Trim, "����", "�ǉ�_���l��ʇD"))
                successFlg = False
            Else
                If arrLine2(105).Trim = "" Then
                    arrLine2(105) = strMei
                End If
            End If
        End If

        intFieldCount = String.Join(",", arrLine2)

        If successFlg Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>�����X���݃`�F�b�N</summary>
    Public Function ChkKameiten(ByVal fupId As Web.UI.WebControls.FileUpload, _
                                ByRef arrCsvLine() As String, _
                                ByRef insCd As String, ByRef updCd As String, ByRef hidInsLineNo As String) As String


        Dim myStream As IO.Stream                           '���o�̓X�g���[��
        Dim myReader As IO.StreamReader                     '�X�g���[�����[�_�[
        Dim strReadLine As String                           '�捞�t�@�C���Ǎ��ݍs
        Dim intLineCount As Integer = 0                     '���C����
        Dim strCsvLine() As String                          'CSV�t�@�C�����e
        Dim strRtn As String = String.Empty

        'CSV���捞�ꍇ
        If arrCsvLine Is Nothing Then
            '���o�̓X�g���[��
            myStream = fupId.FileContent

            '�X�g���[�����[�_�[
            myReader = New IO.StreamReader(myStream, System.Text.Encoding.GetEncoding(932))

            Try
                'CSV�捞
                Do
                    '�捞�t�@�C����ǂݍ���
                    ReDim Preserve strCsvLine(intLineCount)
                    strCsvLine(intLineCount) = myReader.ReadLine()
                    intLineCount += 1
                Loop Until myReader.EndOfStream

                '�捞�t�@�C���ۑ�
                arrCsvLine = strCsvLine

                'CSV�A�b�v���[�h��������`�F�b�N
                For i As Integer = strCsvLine.Length - 1 To 0 Step -1
                    If Not strCsvLine(i).Trim.Equals(String.Empty) Then
                        If i > CsvInputMaxLineCount Then
                            Return String.Format(Messages.Instance.MSG040E, CStr(CsvInputMaxLineCount))
                        ElseIf i < 1 Then
                            Return String.Format(Messages.Instance.MSG048E)
                        Else
                            Exit For
                        End If
                    End If
                Next
            Catch ex As Exception
                Return Messages.Instance.MSG049E
            End Try
        Else
            strCsvLine = arrCsvLine
        End If



        'CSV�t�@�C�����`�F�b�N
        'For i As Integer = intLineNo To strCsvLine.Length - 1
        '    strReadLine = strCsvLine(i)
        '    If (Not strReadLine Is Nothing) AndAlso (Replace(strReadLine, ",", "") <> "") Then


        '        '���۰��̧��.�敪-����۰��̧��.�����X���ށi�� �O0����5���ϊ��j �̑g�����������XϽ��ɑ��݂��Ȃ��ꍇ
        '        If Not String.IsNullOrEmpty(strReadLine.Split(",")(2).Trim) AndAlso _
        '           Not kameitenMasterDA.SelKameitenCd(Right("00000" & strReadLine.Split(",")(2).Trim, 5), strReadLine.Split(",")(1).Trim) Then


        '            intLineNo = i
        '            kbnAndKameitenCd = strReadLine.Split(",")(1).Trim & "," & strReadLine.Split(",")(2).Trim
        '            exitsFlg = kameitenMasterDA.SelKameitenCd(Right("00000" & strReadLine.Split(",")(2).Trim, 5))
        '            If Not exitsFlg Then
        '                hidInsLineNo = hidInsLineNo & i & ","
        '                Return "UnExits"
        '            Else
        '                Return "Err"
        '            End If

        '        End If
        '    End If
        'Next


        'whereStr

        Dim whereStr As New System.Text.StringBuilder

        For i As Integer = 1 To strCsvLine.Length - 1
            strReadLine = strCsvLine(i)
            If (Not strReadLine Is Nothing) AndAlso (Replace(strReadLine, ",", "") <> "") Then
                Dim kameitenCd As String = Right("00000" & strReadLine.Split(",")(2).Trim, 5)
                Dim kbn As String = strReadLine.Split(",")(1).Trim
                If whereStr.Length = 0 Then
                    whereStr.AppendLine(" WHERE (kameiten_cd='" & kameitenCd & "' AND kbn = '" & kbn & "')")
                Else
                    whereStr.AppendLine(" OR (kameiten_cd='" & kameitenCd & "' AND kbn = '" & kbn & "')")
                End If
            End If
        Next


        If whereStr.Length > 0 Then

            Dim dt As DataTable = kameitenMasterDA.SelKameitenCds(whereStr.ToString)
            For i As Integer = 1 To strCsvLine.Length - 1
                strReadLine = strCsvLine(i)
                If (Not strReadLine Is Nothing) AndAlso (Replace(strReadLine, ",", "") <> "") Then
                    Dim kameitenCd As String = Right("00000" & strReadLine.Split(",")(2).Trim, 5)

                    If dt.Select("kameiten_cd='" & kameitenCd & "'").Length > 0 Then
                        If updCd = "" Then
                            updCd = kameitenCd
                        Else
                            updCd = updCd & "," & kameitenCd
                        End If

                    Else
                        If insCd = "" Then
                            insCd = kameitenCd
                        Else
                            insCd = insCd & "," & kameitenCd
                        End If

                        hidInsLineNo = hidInsLineNo & i & ","

                    End If
                End If
            Next
            Return "Success"
        Else
            Return "CSV�f�[�^���Ȃ��ł�"
        End If






    End Function

    ''' <summary>OK���C������</summary>
    Private Sub OkLineSyori(ByVal strLine As String, ByRef dtOk As Data.DataTable)

        'DB���݃`�F�b�N
        If kameitenMasterDA.SelTatouwaribikiSettei(strLine.Split(",")(2).Trim, "") Then
            Me.SetOkDataRow(strLine, dtOk, "1")
        Else
            Me.SetOkDataRow(strLine, dtOk, "0")
        End If

    End Sub

    ''' <summary>OK�f�[�^���C����ǉ�</summary>
    Public Sub SetOkDataRow(ByVal strLine As String, ByRef dt As Data.DataTable, ByVal strInsUpdFlg As String)

        Dim dr As Data.DataRow
        dr = dt.NewRow
        dr.Item("key") = GetDataKey(strLine)
        dr.Item("ins_upd_flg") = strInsUpdFlg
        dr.Item("kbn") = strLine.Split(",")(1).Trim
        dr.Item("kameiten_cd") = strLine.Split(",")(2).Trim
        dr.Item("torikesi") = strLine.Split(",")(3).Trim
        dr.Item("hattyuu_teisi_flg") = strLine.Split(",")(4).Trim
        dr.Item("kameiten_mei1") = strLine.Split(",")(5).Trim
        dr.Item("tenmei_kana1") = strLine.Split(",")(6).Trim
        dr.Item("kameiten_mei2") = strLine.Split(",")(7).Trim
        dr.Item("tenmei_kana2") = strLine.Split(",")(8).Trim
        dr.Item("builder_no") = strLine.Split(",")(9).Trim
        dr.Item("builder_mei") = strLine.Split(",")(10).Trim
        dr.Item("keiretu_cd") = strLine.Split(",")(11).Trim
        dr.Item("keiretu_mei") = strLine.Split(",")(12).Trim
        dr.Item("eigyousyo_cd") = strLine.Split(",")(13).Trim
        dr.Item("eigyousyo_mei") = strLine.Split(",")(14).Trim
        dr.Item("kameiten_seisiki_mei") = strLine.Split(",")(15).Trim
        dr.Item("kameiten_seisiki_mei_kana") = strLine.Split(",")(16).Trim
        dr.Item("todouhuken_cd") = strLine.Split(",")(17).Trim
        dr.Item("todouhuken_mei") = strLine.Split(",")(18).Trim
        '==========��2013/03/07 �ԗ� 407584 �ǉ���======================
        dr.Item("nenkan_tousuu") = strLine.Split(",")(19).Trim '�N�ԓ���
        '==========��2013/03/07 �ԗ� 407584 �ǉ���======================
        dr.Item("fuho_syoumeisyo_flg") = strLine.Split(",")(20).Trim
        dr.Item("fuho_syoumeisyo_kaisi_nengetu") = strLine.Split(",")(21).Trim
        dr.Item("eigyou_tantousya_mei") = strLine.Split(",")(22).Trim
        dr.Item("eigyou_tantousya_simei") = strLine.Split(",")(23).Trim
        dr.Item("kyuu_eigyou_tantousya_mei") = strLine.Split(",")(24).Trim
        dr.Item("kyuu_eigyou_tantousya_simei") = strLine.Split(",")(25).Trim
        '==========��2013/03/07 �ԗ� 407584 �ǉ���======================
        dr.Item("koj_uri_syubetsu") = strLine.Split(",")(26).Trim '�H��������
        dr.Item("koj_uri_syubetsu_mei") = strLine.Split(",")(27).Trim '�H�������ʖ�
        dr.Item("jiosaki_flg") = strLine.Split(",")(28).Trim 'JIO��t���O
        '==========��2013/03/07 �ԗ� 407584 �ǉ���======================
        dr.Item("kaiyaku_haraimodosi_kkk") = strLine.Split(",")(29).Trim
        dr.Item("syouhin_cd1") = strLine.Split(",")(30).Trim
        dr.Item("syouhin_mei1") = strLine.Split(",")(31).Trim
        dr.Item("syouhin_cd2") = strLine.Split(",")(32).Trim
        dr.Item("syouhin_mei2") = strLine.Split(",")(33).Trim
        dr.Item("syouhin_cd3") = strLine.Split(",")(34).Trim
        dr.Item("syouhin_mei3") = strLine.Split(",")(35).Trim
        dr.Item("tys_seikyuu_saki_kbn") = strLine.Split(",")(36).Trim
        dr.Item("tys_seikyuu_saki_cd") = strLine.Split(",")(37).Trim
        dr.Item("tys_seikyuu_saki_brc") = strLine.Split(",")(38).Trim
        dr.Item("tys_seikyuu_saki_mei") = strLine.Split(",")(39).Trim
        dr.Item("koj_seikyuu_saki_kbn") = strLine.Split(",")(40).Trim
        dr.Item("koj_seikyuu_saki_cd") = strLine.Split(",")(41).Trim
        dr.Item("koj_seikyuu_saki_brc") = strLine.Split(",")(42).Trim
        dr.Item("koj_seikyuu_saki_mei") = strLine.Split(",")(43).Trim
        dr.Item("hansokuhin_seikyuu_saki_kbn") = strLine.Split(",")(44).Trim
        dr.Item("hansokuhin_seikyuu_saki_cd") = strLine.Split(",")(45).Trim
        dr.Item("hansokuhin_seikyuu_saki_brc") = strLine.Split(",")(46).Trim
        dr.Item("hansokuhin_seikyuu_saki_mei") = strLine.Split(",")(47).Trim
        dr.Item("tatemono_seikyuu_saki_kbn") = strLine.Split(",")(48).Trim
        dr.Item("tatemono_seikyuu_saki_cd") = strLine.Split(",")(49).Trim
        dr.Item("tatemono_seikyuu_saki_brc") = strLine.Split(",")(50).Trim
        dr.Item("tatemono_seikyuu_saki_mei") = strLine.Split(",")(51).Trim
        dr.Item("seikyuu_saki_kbn5") = strLine.Split(",")(52).Trim
        dr.Item("seikyuu_saki_cd5") = strLine.Split(",")(53).Trim
        dr.Item("seikyuu_saki_brc5") = strLine.Split(",")(54).Trim
        dr.Item("seikyuu_saki_mei5") = strLine.Split(",")(55).Trim
        dr.Item("seikyuu_saki_kbn6") = strLine.Split(",")(56).Trim
        dr.Item("seikyuu_saki_cd6") = strLine.Split(",")(57).Trim
        dr.Item("seikyuu_saki_brc6") = strLine.Split(",")(58).Trim
        dr.Item("seikyuu_saki_mei6") = strLine.Split(",")(59).Trim
        dr.Item("seikyuu_saki_kbn7") = strLine.Split(",")(60).Trim
        dr.Item("seikyuu_saki_cd7") = strLine.Split(",")(61).Trim
        dr.Item("seikyuu_saki_brc7") = strLine.Split(",")(62).Trim
        dr.Item("seikyuu_saki_mei7") = strLine.Split(",")(63).Trim
        '==========��2013/03/07 �ԗ� 407584 �ǉ���======================
        dr.Item("hosyou_kikan") = strLine.Split(",")(64).Trim '�ۏ؊���
        dr.Item("hosyousyo_hak_umu") = strLine.Split(",")(65).Trim '�ۏ؏����s�L��
        '==========��2013/03/07 �ԗ� 407584 �ǉ���======================
        dr.Item("nyuukin_kakunin_jyouken") = strLine.Split(",")(66).Trim
        dr.Item("nyuukin_kakunin_oboegaki") = strLine.Split(",")(67).Trim
        dr.Item("tys_mitsyo_flg") = strLine.Split(",")(68).Trim
        dr.Item("hattyuusyo_flg") = strLine.Split(",")(69).Trim
        dr.Item("yuubin_no") = strLine.Split(",")(70).Trim
        dr.Item("jyuusyo1") = strLine.Split(",")(71).Trim
        dr.Item("jyuusyo2") = strLine.Split(",")(72).Trim
        dr.Item("jyuusyo_cd") = strLine.Split(",")(73).Trim
        dr.Item("jyuusyo_mei") = strLine.Split(",")(74).Trim
        dr.Item("busyo_mei") = strLine.Split(",")(75).Trim
        dr.Item("daihyousya_mei") = strLine.Split(",")(76).Trim
        dr.Item("tel_no") = strLine.Split(",")(77).Trim
        dr.Item("fax_no") = strLine.Split(",")(78).Trim
        dr.Item("mail_address") = strLine.Split(",")(79).Trim
        dr.Item("bikou1") = strLine.Split(",")(80).Trim
        dr.Item("bikou2") = strLine.Split(",")(81).Trim
        '==========��2013/03/07 �ԗ� 407584 �ǉ���======================
        dr.Item("add_date") = strLine.Split(",")(82).Trim '�o�^��
        dr.Item("seikyuu_umu") = strLine.Split(",")(83).Trim '�����L��
        dr.Item("syouhin_cd") = strLine.Split(",")(84).Trim '���i�R�[�h
        dr.Item("syouhin_mei") = strLine.Split(",")(85).Trim '���i��
        dr.Item("uri_gaku") = strLine.Split(",")(86).Trim '������z
        dr.Item("koumuten_seikyuu_gaku") = strLine.Split(",")(87).Trim '�H���X�������z
        dr.Item("seikyuusyo_hak_date") = strLine.Split(",")(88).Trim '���������s��
        dr.Item("uri_date") = strLine.Split(",")(89).Trim '����N����
        dr.Item("bikou") = strLine.Split(",")(90).Trim '���l
        '==========��2013/03/07 �ԗ� 407584 �ǉ���======================
        dr.Item("bikou_syubetu1") = strLine.Split(",")(96).Trim
        dr.Item("bikou_syubetu1_mei") = strLine.Split(",")(97).Trim
        dr.Item("bikou_syubetu2") = strLine.Split(",")(98).Trim
        dr.Item("bikou_syubetu2_mei") = strLine.Split(",")(99).Trim
        dr.Item("bikou_syubetu3") = strLine.Split(",")(100).Trim
        dr.Item("bikou_syubetu3_mei") = strLine.Split(",")(101).Trim
        dr.Item("bikou_syubetu4") = strLine.Split(",")(102).Trim
        dr.Item("bikou_syubetu4_mei") = strLine.Split(",")(103).Trim
        dr.Item("bikou_syubetu5") = strLine.Split(",")(104).Trim
        dr.Item("bikou_syubetu5_mei") = strLine.Split(",")(105).Trim
        dr.Item("naiyou1") = strLine.Split(",")(106).Trim
        dr.Item("naiyou2") = strLine.Split(",")(107).Trim
        dr.Item("naiyou3") = strLine.Split(",")(108).Trim
        dr.Item("naiyou4") = strLine.Split(",")(109).Trim
        dr.Item("naiyou5") = strLine.Split(",")(110).Trim

        dr.Item("ssgr_kkk") = strLine.Split(",")(111).Trim
        dr.Item("kaiseki_hosyou_kkk") = strLine.Split(",")(112).Trim
        dr.Item("koj_mitiraisyo_soufu_fuyou") = strLine.Split(",")(113).Trim
        dr.Item("hikiwatasi_inji_umu") = strLine.Split(",")(114).Trim
        dr.Item("hosyousyo_hassou_umu") = strLine.Split(",")(115).Trim
        dr.Item("ekijyouka_tokuyaku_kakaku") = strLine.Split(",")(116).Trim
        dr.Item("hosyousyo_hassou_umu_start_date") = strLine.Split(",")(117).Trim
        dr.Item("taiou_syouhin_kbn") = strLine.Split(",")(118).Trim
        dr.Item("taiou_syouhin_kbn_set_date") = strLine.Split(",")(119).Trim
        dr.Item("campaign_waribiki_flg") = strLine.Split(",")(120).Trim
        dr.Item("campaign_waribiki_set_date") = strLine.Split(",")(121).Trim
        dr.Item("online_waribiki_flg") = strLine.Split(",")(122).Trim
        dr.Item("b_str_yuuryou_wide_flg") = strLine.Split(",")(123).Trim



        dr.Item("add_login_user_id") = userId
        dr.Item("upd_login_user_id") = userId

        dt.Rows.Add(dr)
    End Sub

    ''' <summary>�G���[���C������</summary>
    Private Sub ErrorLineSyori(ByVal intLineNo As Integer, ByVal strLine As String, ByRef dtError As Data.DataTable)

        Dim intMaxCount As Integer

        Dim dr As Data.DataRow
        dr = dtError.NewRow

        '�ő�t�B�[���h�ݒ�
        If strLine.Split(",").Length < CsvInputCheck.KAMEITEN_FIELD_COUNT Then
            intMaxCount = strLine.Split(",").Length
        Else
            intMaxCount = CsvInputCheck.KAMEITEN_FIELD_COUNT
        End If

        For i As Integer = 0 To intMaxCount - 1
            '�ő咷��؂���
            dr.Item(i) = commonCheck.CutMaxLength(strLine.Split(",")(i).Trim, commonCheck.KAMEITEN_MAX_LENGTH(i))
        Next

        '�s��
        dr.Item(CsvInputCheck.KAMEITEN_FIELD_COUNT) = CStr(intLineNo)
        '��������
        dr.Item(CsvInputCheck.KAMEITEN_FIELD_COUNT + 1) = InputDate
        '�o�^���O�C�����[�UID
        dr.Item(CsvInputCheck.KAMEITEN_FIELD_COUNT + 2) = userId

        dtError.Rows.Add(dr)

    End Sub

    ''' <summary>�����X���i�������@���ʑΉ��}�X�^���쐬</summary>
    Public Sub CreateOkDataTable(ByRef dtOk As Data.DataTable)
        dtOk.Columns.Add("key")                                      '--�L�[(�����X�R�[�h)
        dtOk.Columns.Add("ins_upd_flg")                              '--�ǉ��X�VFLG(0:�ǉ�; 1:�X�V)
        dtOk.Columns.Add("kbn")                                      '�敪
        dtOk.Columns.Add("kameiten_cd")                              '�����X�R�[�h
        dtOk.Columns.Add("torikesi")                                 '���
        dtOk.Columns.Add("hattyuu_teisi_flg")                        '������~�t���O
        dtOk.Columns.Add("kameiten_mei1")                            '�����X��1
        dtOk.Columns.Add("tenmei_kana1")                             '�X���J�i1
        dtOk.Columns.Add("kameiten_mei2")                            '�����X��2
        dtOk.Columns.Add("tenmei_kana2")                             '�X���J�i2
        dtOk.Columns.Add("builder_no")                               '�r���_�[NO
        dtOk.Columns.Add("builder_mei")                              '�r���_�[��
        dtOk.Columns.Add("keiretu_cd")                               '�n��R�[�h
        dtOk.Columns.Add("keiretu_mei")                              '�n��
        dtOk.Columns.Add("eigyousyo_cd")                             '�c�Ə��R�[�h
        dtOk.Columns.Add("eigyousyo_mei")                            '�c�Ə���
        dtOk.Columns.Add("kameiten_seisiki_mei")                     '��������
        dtOk.Columns.Add("kameiten_seisiki_mei_kana")                '��������2
        dtOk.Columns.Add("todouhuken_cd")                            '�s���{���R�[�h
        dtOk.Columns.Add("todouhuken_mei")                           '�s���{����
        '==========��2013/03/07 �ԗ� 407584 �ǉ���======================
        dtOk.Columns.Add("nenkan_tousuu")                            '�N�ԓ���
        '==========��2013/03/07 �ԗ� 407584 �ǉ���======================
        dtOk.Columns.Add("fuho_syoumeisyo_flg")                      '�t�ۏؖ�FLG
        dtOk.Columns.Add("fuho_syoumeisyo_kaisi_nengetu")            '�t�ۏؖ����J�n�N��
        dtOk.Columns.Add("eigyou_tantousya_mei")                     '�c�ƒS����
        dtOk.Columns.Add("eigyou_tantousya_simei")                   '�c�ƒS���Җ�
        dtOk.Columns.Add("kyuu_eigyou_tantousya_mei")                '���c�ƒS����
        dtOk.Columns.Add("kyuu_eigyou_tantousya_simei")              '���c�ƒS���Җ�
        '==========��2013/03/07 �ԗ� 407584 �ǉ���======================
        dtOk.Columns.Add("koj_uri_syubetsu")                         '�H��������
        dtOk.Columns.Add("koj_uri_syubetsu_mei")                     '�H�������ʖ�
        dtOk.Columns.Add("jiosaki_flg")                              'JIO��t���O
        '==========��2013/03/07 �ԗ� 407584 �ǉ���======================
        dtOk.Columns.Add("kaiyaku_haraimodosi_kkk")                  '��񕥖߉��i
        dtOk.Columns.Add("syouhin_cd1")                              '���敪1���i�R�[�h
        dtOk.Columns.Add("syouhin_mei1")                             '���敪1���i��
        dtOk.Columns.Add("syouhin_cd2")                              '���敪2���i�R�[�h
        dtOk.Columns.Add("syouhin_mei2")                             '���敪2���i��
        dtOk.Columns.Add("syouhin_cd3")                              '���敪3���i�R�[�h
        dtOk.Columns.Add("syouhin_mei3")                             '���敪3���i��
        dtOk.Columns.Add("tys_seikyuu_saki_kbn")                     '����������敪
        dtOk.Columns.Add("tys_seikyuu_saki_cd")                      '����������R�[�h
        dtOk.Columns.Add("tys_seikyuu_saki_brc")                     '����������}��
        dtOk.Columns.Add("tys_seikyuu_saki_mei")                     '���������於
        dtOk.Columns.Add("koj_seikyuu_saki_kbn")                     '�H��������敪
        dtOk.Columns.Add("koj_seikyuu_saki_cd")                      '�H��������R�[�h
        dtOk.Columns.Add("koj_seikyuu_saki_brc")                     '�H��������}��
        dtOk.Columns.Add("koj_seikyuu_saki_mei")                     '�H�������於
        dtOk.Columns.Add("hansokuhin_seikyuu_saki_kbn")              '�̑��i������敪
        dtOk.Columns.Add("hansokuhin_seikyuu_saki_cd")               '�̑��i������R�[�h
        dtOk.Columns.Add("hansokuhin_seikyuu_saki_brc")              '�̑��i������}��
        dtOk.Columns.Add("hansokuhin_seikyuu_saki_mei")              '�̑��i�����於
        dtOk.Columns.Add("tatemono_seikyuu_saki_kbn")                '��������������敪
        dtOk.Columns.Add("tatemono_seikyuu_saki_cd")                 '��������������R�[�h
        dtOk.Columns.Add("tatemono_seikyuu_saki_brc")                '��������������}��
        dtOk.Columns.Add("tatemono_seikyuu_saki_mei")                '�������������於
        dtOk.Columns.Add("seikyuu_saki_kbn5")                        '������敪5
        dtOk.Columns.Add("seikyuu_saki_cd5")                         '������R�[�h5
        dtOk.Columns.Add("seikyuu_saki_brc5")                        '������}��5
        dtOk.Columns.Add("seikyuu_saki_mei5")                        '�����於5
        dtOk.Columns.Add("seikyuu_saki_kbn6")                        '������敪6
        dtOk.Columns.Add("seikyuu_saki_cd6")                         '������R�[�h6
        dtOk.Columns.Add("seikyuu_saki_brc6")                        '������}��6
        dtOk.Columns.Add("seikyuu_saki_mei6")                        '�����於6
        dtOk.Columns.Add("seikyuu_saki_kbn7")                        '������敪7
        dtOk.Columns.Add("seikyuu_saki_cd7")                         '������R�[�h7
        dtOk.Columns.Add("seikyuu_saki_brc7")                        '������}��7
        dtOk.Columns.Add("seikyuu_saki_mei7")                        '�����於7
        '==========��2013/03/07 �ԗ� 407584 �ǉ���======================
        dtOk.Columns.Add("hosyou_kikan")                             '�ۏ؊���
        dtOk.Columns.Add("hosyousyo_hak_umu")                        '�ۏ؏����s�L��
        '==========��2013/03/07 �ԗ� 407584 �ǉ���======================
        dtOk.Columns.Add("nyuukin_kakunin_jyouken")                  '�����m�F����
        dtOk.Columns.Add("nyuukin_kakunin_oboegaki")                 '�����m�F�o��
        dtOk.Columns.Add("tys_mitsyo_flg")                           '�������Ϗ�FLG
        dtOk.Columns.Add("hattyuusyo_flg")                           '������FLG                         
        dtOk.Columns.Add("yuubin_no")                                '�X�֔ԍ�
        dtOk.Columns.Add("jyuusyo1")                                 '�Z��1
        dtOk.Columns.Add("jyuusyo2")                                 '�Z��2
        dtOk.Columns.Add("jyuusyo_cd")                               '���ݒn�R�[�h
        dtOk.Columns.Add("jyuusyo_mei")                              '���ݒn��
        dtOk.Columns.Add("busyo_mei")                                '������
        dtOk.Columns.Add("daihyousya_mei")                           '��\�Җ�
        dtOk.Columns.Add("tel_no")                                   '�d�b�ԍ�
        dtOk.Columns.Add("fax_no")                                   'FAX�ԍ�
        dtOk.Columns.Add("mail_address")                             '�\���S����
        dtOk.Columns.Add("bikou1")                                   '���l1
        dtOk.Columns.Add("bikou2")                                   '���l2
        '==========��2013/03/07 �ԗ� 407584 �ǉ���======================
        dtOk.Columns.Add("add_date")                                 '�o�^��
        dtOk.Columns.Add("seikyuu_umu")                              '�����L��
        dtOk.Columns.Add("syouhin_cd")                               '���i�R�[�h
        dtOk.Columns.Add("syouhin_mei")                              '���i��
        dtOk.Columns.Add("uri_gaku")                                 '������z
        dtOk.Columns.Add("koumuten_seikyuu_gaku")                    '�H���X�������z
        dtOk.Columns.Add("seikyuusyo_hak_date")                      '���������s��
        dtOk.Columns.Add("uri_date")                                 '����N����
        dtOk.Columns.Add("bikou")                                    '���l
        '==========��2013/03/07 �ԗ� 407584 �ǉ���======================
        dtOk.Columns.Add("bikou_syubetu1")                           '�ǉ�_���l��ʇ@
        dtOk.Columns.Add("bikou_syubetu1_mei")                       '�ǉ�_���l��ʇ@��
        dtOk.Columns.Add("bikou_syubetu2")                           '�ǉ�_���l��ʇA
        dtOk.Columns.Add("bikou_syubetu2_mei")                       '�ǉ�_���l��ʇA��
        dtOk.Columns.Add("bikou_syubetu3")                           '�ǉ�_���l��ʇB
        dtOk.Columns.Add("bikou_syubetu3_mei")                       '�ǉ�_���l��ʇB��
        dtOk.Columns.Add("bikou_syubetu4")                           '�ǉ�_���l��ʇC
        dtOk.Columns.Add("bikou_syubetu4_mei")                       '�ǉ�_���l��ʇC��
        dtOk.Columns.Add("bikou_syubetu5")                           '�ǉ�_���l��ʇD
        dtOk.Columns.Add("bikou_syubetu5_mei")                       '�ǉ�_���l��ʇD��
        dtOk.Columns.Add("naiyou1")                                  '�ǉ�_���e�@
        dtOk.Columns.Add("naiyou2")                                  '�ǉ�_���e�A
        dtOk.Columns.Add("naiyou3")                                  '�ǉ�_���e�B
        dtOk.Columns.Add("naiyou4")                                  '�ǉ�_���e�C
        dtOk.Columns.Add("naiyou5")                                  '�ǉ�_���e�D
        dtOk.Columns.Add("syori_datetime")                           '--��������
        dtOk.Columns.Add("add_login_user_id")                        '--�o�^��ID
        dtOk.Columns.Add("add_datetime")                             '--�o�^����
        dtOk.Columns.Add("upd_login_user_id")                        '--�X�V��ID
        dtOk.Columns.Add("upd_datetime")                             '--�X�V����


        dtOk.Columns.Add("ssgr_kkk") 'SSGR���i
        dtOk.Columns.Add("kaiseki_hosyou_kkk") '��͕ۏ؉��i
        dtOk.Columns.Add("koj_mitiraisyo_soufu_fuyou") '�H�����ψ˗������t�s�v
        dtOk.Columns.Add("hikiwatasi_inji_umu") '�ۏ؏����n���󎚗L��
        dtOk.Columns.Add("hosyousyo_hassou_umu") '�ۏ؏��������@
        dtOk.Columns.Add("ekijyouka_tokuyaku_kakaku") '�t�󉻓����
        dtOk.Columns.Add("hosyousyo_hassou_umu_start_date") '�ۏ؏��������@_�K�p�J�n��
        dtOk.Columns.Add("taiou_syouhin_kbn") '�Ή����i�敪
        dtOk.Columns.Add("taiou_syouhin_kbn_set_date") '�Ή����i�敪�ݒ��
        dtOk.Columns.Add("campaign_waribiki_flg") '�L�����y�[������FLG
        dtOk.Columns.Add("campaign_waribiki_set_date") '�L�����y�[�������ݒ��
        dtOk.Columns.Add("online_waribiki_flg") '�I�����C������FLG
        dtOk.Columns.Add("b_str_yuuryou_wide_flg") 'B-STR�L�����C�hFLG


    End Sub

    ''' <summary>�����X���ꊇ�捞�G���[�}�X�^���쐬</summary>
    Public Sub CreateErrorDataTable(ByRef dtError As Data.DataTable)
        dtError.Columns.Add("edi_jouhou_sakusei_date")                  '--EDI���쐬��
        dtError.Columns.Add("kbn")                                      '�敪
        dtError.Columns.Add("kameiten_cd")                              '�����X�R�[�h
        dtError.Columns.Add("torikesi")                                 '���
        dtError.Columns.Add("hattyuu_teisi_flg")                        '������~�t���O
        dtError.Columns.Add("kameiten_mei1")                            '�����X��1
        dtError.Columns.Add("tenmei_kana1")                             '�X���J�i1
        dtError.Columns.Add("kameiten_mei2")                            '�����X��2
        dtError.Columns.Add("tenmei_kana2")                             '�X���J�i2
        dtError.Columns.Add("builder_no")                               '�r���_�[NO
        dtError.Columns.Add("builder_mei")                              '�r���_�[��
        dtError.Columns.Add("keiretu_cd")                               '�n��R�[�h
        dtError.Columns.Add("keiretu_mei")                              '�n��
        dtError.Columns.Add("eigyousyo_cd")                             '�c�Ə��R�[�h
        dtError.Columns.Add("eigyousyo_mei")                            '�c�Ə���
        dtError.Columns.Add("kameiten_seisiki_mei")                     '��������
        dtError.Columns.Add("kameiten_seisiki_mei_kana")                '��������2
        dtError.Columns.Add("todouhuken_cd")                            '�s���{���R�[�h
        dtError.Columns.Add("todouhuken_mei")                           '�s���{����
        '==========��2013/03/07 �ԗ� 407584 �ǉ���======================
        dtError.Columns.Add("nenkan_tousuu")                            '�N�ԓ���
        '==========��2013/03/07 �ԗ� 407584 �ǉ���======================
        dtError.Columns.Add("fuho_syoumeisyo_flg")                      '�t�ۏؖ�FLG
        dtError.Columns.Add("fuho_syoumeisyo_kaisi_nengetu")            '�t�ۏؖ����J�n�N��
        dtError.Columns.Add("eigyou_tantousya_mei")                     '�c�ƒS����
        dtError.Columns.Add("eigyou_tantousya_simei")                   '�c�ƒS���Җ�
        dtError.Columns.Add("kyuu_eigyou_tantousya_mei")                '���c�ƒS����
        dtError.Columns.Add("kyuu_eigyou_tantousya_simei")              '���c�ƒS���Җ�
        '==========��2013/03/07 �ԗ� 407584 �ǉ���======================
        dtError.Columns.Add("koj_uri_syubetsu")                         '�H��������
        dtError.Columns.Add("koj_uri_syubetsu_mei")                     '�H�������ʖ�
        dtError.Columns.Add("jiosaki_flg")                              'JIO��t���O
        '==========��2013/03/07 �ԗ� 407584 �ǉ���======================
        dtError.Columns.Add("kaiyaku_haraimodosi_kkk")                  '��񕥖߉��i
        dtError.Columns.Add("syouhin_cd1")                              '���敪1���i�R�[�h
        dtError.Columns.Add("syouhin_mei1")                             '���敪1���i��
        dtError.Columns.Add("syouhin_cd2")                              '���敪2���i�R�[�h
        dtError.Columns.Add("syouhin_mei2")                             '���敪2���i��
        dtError.Columns.Add("syouhin_cd3")                              '���敪3���i�R�[�h
        dtError.Columns.Add("syouhin_mei3")                             '���敪3���i��
        dtError.Columns.Add("tys_seikyuu_saki_kbn")                     '����������敪
        dtError.Columns.Add("tys_seikyuu_saki_cd")                      '����������R�[�h
        dtError.Columns.Add("tys_seikyuu_saki_brc")                     '����������}��
        dtError.Columns.Add("tys_seikyuu_saki_mei")                     '���������於
        dtError.Columns.Add("koj_seikyuu_saki_kbn")                     '�H��������敪
        dtError.Columns.Add("koj_seikyuu_saki_cd")                      '�H��������R�[�h
        dtError.Columns.Add("koj_seikyuu_saki_brc")                     '�H��������}��
        dtError.Columns.Add("koj_seikyuu_saki_mei")                     '�H�������於
        dtError.Columns.Add("hansokuhin_seikyuu_saki_kbn")              '�̑��i������敪
        dtError.Columns.Add("hansokuhin_seikyuu_saki_cd")               '�̑��i������R�[�h
        dtError.Columns.Add("hansokuhin_seikyuu_saki_brc")              '�̑��i������}��
        dtError.Columns.Add("hansokuhin_seikyuu_saki_mei")              '�̑��i�����於
        dtError.Columns.Add("tatemono_seikyuu_saki_kbn")                '��������������敪
        dtError.Columns.Add("tatemono_seikyuu_saki_cd")                 '��������������R�[�h
        dtError.Columns.Add("tatemono_seikyuu_saki_brc")                '��������������}��
        dtError.Columns.Add("tatemono_seikyuu_saki_mei")                '�������������於
        dtError.Columns.Add("seikyuu_saki_kbn5")                        '������敪5
        dtError.Columns.Add("seikyuu_saki_cd5")                         '������R�[�h5
        dtError.Columns.Add("seikyuu_saki_brc5")                        '������}��5
        dtError.Columns.Add("seikyuu_saki_mei5")                        '�����於5
        dtError.Columns.Add("seikyuu_saki_kbn6")                        '������敪6
        dtError.Columns.Add("seikyuu_saki_cd6")                         '������R�[�h6
        dtError.Columns.Add("seikyuu_saki_brc6")                        '������}��6
        dtError.Columns.Add("seikyuu_saki_mei6")                        '�����於6
        dtError.Columns.Add("seikyuu_saki_kbn7")                        '������敪7
        dtError.Columns.Add("seikyuu_saki_cd7")                         '������R�[�h7
        dtError.Columns.Add("seikyuu_saki_brc7")                        '������}��7
        dtError.Columns.Add("seikyuu_saki_mei7")                        '�����於7
        '==========��2013/03/07 �ԗ� 407584 �ǉ���======================
        dtError.Columns.Add("hosyou_kikan")                             '�ۏ؊���
        dtError.Columns.Add("hosyousyo_hak_umu")                        '�ۏ؏����s�L��
        '==========��2013/03/07 �ԗ� 407584 �ǉ���======================
        dtError.Columns.Add("nyuukin_kakunin_jyouken")                  '�����m�F����
        dtError.Columns.Add("nyuukin_kakunin_oboegaki")                 '�����m�F�o��
        dtError.Columns.Add("tys_mitsyo_flg")                           '�������Ϗ�FLG
        dtError.Columns.Add("hattyuusyo_flg")                           '������FLG                         
        dtError.Columns.Add("yuubin_no")                                '�X�֔ԍ�
        dtError.Columns.Add("jyuusyo1")                                 '�Z��1
        dtError.Columns.Add("jyuusyo2")                                 '�Z��2
        dtError.Columns.Add("jyuusyo_cd")                               '���ݒn�R�[�h
        dtError.Columns.Add("jyuusyo_mei")                              '���ݒn��
        dtError.Columns.Add("busyo_mei")                                '������
        dtError.Columns.Add("daihyousya_mei")                           '��\�Җ�
        dtError.Columns.Add("tel_no")                                   '�d�b�ԍ�
        dtError.Columns.Add("fax_no")                                   'FAX�ԍ�
        dtError.Columns.Add("mail_address")                             '�\���S����
        dtError.Columns.Add("bikou1")                                   '���l1
        dtError.Columns.Add("bikou2")                                   '���l2
        '==========��2013/03/07 �ԗ� 407584 �ǉ���======================
        dtError.Columns.Add("add_date")                                 '�o�^��
        dtError.Columns.Add("seikyuu_umu")                              '�����L��
        dtError.Columns.Add("syouhin_cd")                               '���i�R�[�h
        dtError.Columns.Add("syouhin_mei")                              '���i��
        dtError.Columns.Add("uri_gaku")                                 '������z
        dtError.Columns.Add("koumuten_seikyuu_gaku")                    '�H���X�������z
        dtError.Columns.Add("seikyuusyo_hak_date")                      '���������s��
        dtError.Columns.Add("uri_date")                                 '����N����
        dtError.Columns.Add("bikou")                                    '���l
        '==========��2013/03/07 �ԗ� 407584 �ǉ���======================
        '==========2012/05/09 �ԗ� 407553�̑Ή� �ǉ���======================
        dtError.Columns.Add("kameiten_upd_datetime")                    '�����X_�X�V����
        dtError.Columns.Add("tatouwari_upd_datetime_1")                 '��������_�X�V����1
        dtError.Columns.Add("tatouwari_upd_datetime_2")                 '��������_�X�V����2
        dtError.Columns.Add("tatouwari_upd_datetime_3")                 '��������_�X�V����3
        dtError.Columns.Add("kameiten_jyuusyo_upd_datetime")            '�����X�Z��_�X�V����
        '==========2012/05/09 �ԗ� 407553�̑Ή� �ǉ���======================
        dtError.Columns.Add("bikou_syubetu1")                           '�ǉ�_���l��ʇ@
        dtError.Columns.Add("bikou_syubetu1_mei")                       '�ǉ�_���l��ʇ@��
        dtError.Columns.Add("bikou_syubetu2")                           '�ǉ�_���l��ʇA
        dtError.Columns.Add("bikou_syubetu2_mei")                       '�ǉ�_���l��ʇA��
        dtError.Columns.Add("bikou_syubetu3")                           '�ǉ�_���l��ʇB
        dtError.Columns.Add("bikou_syubetu3_mei")                       '�ǉ�_���l��ʇB��
        dtError.Columns.Add("bikou_syubetu4")                           '�ǉ�_���l��ʇC
        dtError.Columns.Add("bikou_syubetu4_mei")                       '�ǉ�_���l��ʇC��
        dtError.Columns.Add("bikou_syubetu5")                           '�ǉ�_���l��ʇD
        dtError.Columns.Add("bikou_syubetu5_mei")                       '�ǉ�_���l��ʇD��
        dtError.Columns.Add("naiyou1")                                  '�ǉ�_���e�@
        dtError.Columns.Add("naiyou2")                                  '�ǉ�_���e�A
        dtError.Columns.Add("naiyou3")                                  '�ǉ�_���e�B
        dtError.Columns.Add("naiyou4")                                  '�ǉ�_���e�C
        dtError.Columns.Add("naiyou5")                                  '�ǉ�_���e�D
        dtError.Columns.Add("gyou_no")                                  '--�sNO
        dtError.Columns.Add("syori_datetime")                           '--��������
        dtError.Columns.Add("add_login_user_id")                        '--�o�^��ID
        dtError.Columns.Add("add_datetime")                             '--�o�^����
        dtError.Columns.Add("upd_login_user_id")                        '--�X�V��ID
        dtError.Columns.Add("upd_datetime")                             '--�X�V����

        dtError.Columns.Add("ssgr_kkk") 'SSGR���i
        dtError.Columns.Add("kaiseki_hosyou_kkk") '��͕ۏ؉��i
        dtError.Columns.Add("koj_mitiraisyo_soufu_fuyou") '�H�����ψ˗������t�s�v
        dtError.Columns.Add("hikiwatasi_inji_umu") '�ۏ؏����n���󎚗L��
        dtError.Columns.Add("hosyousyo_hassou_umu") '�ۏ؏��������@
        dtError.Columns.Add("ekijyouka_tokuyaku_kakaku") '�t�󉻓����
        dtError.Columns.Add("hosyousyo_hassou_umu_start_date") '�ۏ؏��������@_�K�p�J�n��
        dtError.Columns.Add("taiou_syouhin_kbn") '�Ή����i�敪
        dtError.Columns.Add("taiou_syouhin_kbn_set_date") '�Ή����i�敪�ݒ��
        dtError.Columns.Add("campaign_waribiki_flg") '�L�����y�[������FLG
        dtError.Columns.Add("campaign_waribiki_set_date") '�L�����y�[�������ݒ��
        dtError.Columns.Add("online_waribiki_flg") '�I�����C������FLG
        dtError.Columns.Add("b_str_yuuryou_wide_flg") 'B-STR�L�����C�hFLG

    End Sub

    ''' <summary>OK�f�[�^��key��ݒ�</summary>
    Public Function GetDataKey(ByVal strLine As String) As String

        Return strLine.Split(",")(1).Trim & "," & strLine.Split(",")(2).Trim & "," & strLine.Split(",")(3).Trim & "," & strLine.Split(",")(5).Trim & "," & strLine.Split(",")(6).Trim

    End Function

    Private Function SuccessSyori(ByVal dtOk As Data.DataTable, ByVal dtInsKameiten As Data.DataTable) As Boolean

        Dim successFlg As Boolean = False

        '�����X�}�X�^�X�V
        successFlg = kameitenMasterDA.UpdKameiten(dtOk)
        If Not successFlg Then
            Return successFlg
        End If
        successFlg = kameitenMasterDA.InsKameiten(dtInsKameiten)
        If Not successFlg Then
            Return successFlg
        End If

        '==========��2013/03/08 �ԗ� 407584 �ǉ���======================
        '������}�X�^
        successFlg = kameitenMasterDA.InsUpdSeikyuuSaki(dtOk)
        If Not successFlg Then
            Return successFlg
        End If
        successFlg = kameitenMasterDA.InsUpdSeikyuuSaki(dtInsKameiten)
        If Not successFlg Then
            Return successFlg
        End If
        '==========��2013/03/08 �ԗ� 407584 �ǉ���======================

        '�����X�Z���}�X�^�X�V
        successFlg = kameitenMasterDA.UpdKameitenJyuusyo(dtOk)
        If Not successFlg Then
            Return successFlg
        End If
        successFlg = kameitenMasterDA.UpdKameitenJyuusyo(dtInsKameiten)
        If Not successFlg Then
            Return successFlg
        End If

        '���������ݒ�}�X�^�@�ց@���敪 ���ƂɍX�V�E�o�^���s���B
        successFlg = kameitenMasterDA.InsUpdTatouwaribikiSettei(dtOk)
        If Not successFlg Then
            Return successFlg
        End If
        successFlg = kameitenMasterDA.InsUpdTatouwaribikiSettei(dtInsKameiten)
        If Not successFlg Then
            Return successFlg
        End If

        '==========��2013/03/08 �ԗ� 407584 �ǉ���======================
        '�X�ʏ��������e�[�u��
        successFlg = kameitenMasterDA.InsTenbetuSyokiSeikyuu(dtOk)
        If Not successFlg Then
            Return successFlg
        End If
        successFlg = kameitenMasterDA.InsTenbetuSyokiSeikyuu(dtInsKameiten)
        If Not successFlg Then
            Return successFlg
        End If
        '==========��2013/03/08 �ԗ� 407584 �ǉ���======================

        '�����X���l�ݒ�}�X�^�@�ց@���l���(�@�`�D) ���Ƃɒǉ��o�^���s���B
        successFlg = kameitenMasterDA.InsKameitenBikouSettei(dtOk)
        If Not successFlg Then
            Return successFlg
        End If
        successFlg = kameitenMasterDA.InsKameitenBikouSettei(dtInsKameiten)
        If Not successFlg Then
            Return successFlg
        End If

        Return successFlg

    End Function

    ''' <summary>�����X���ꊇ�捞�G���[�f�[�^���擾</summary>
    Public Function GetKameitenInfoIttukatuError(ByVal strEdidate As String, ByVal strSyoridate As String) As Data.DataTable
        Return kameitenMasterDA.SelKameitenInfoIttukatuError(strEdidate, strSyoridate)
    End Function

    ''' <summary>�����X���ꊇ�捞�G���[�������擾</summary>
    Public Function GetKameitenInfoIttukatuErrorCount(ByVal strEdidate As String, ByVal strSyoridate As String) As Integer
        Return kameitenMasterDA.SelKameitenInfoIttukatuErrorCount(strEdidate, strSyoridate)
    End Function

    ''' <summary>�����X���ꊇ�捞�G���[CSV�f�[�^���擾</summary>
    Public Function GetKameitenInfoIttukatuErrorCsv(ByVal strEdidate As String, ByVal strSyoridate As String) As Data.DataTable
        Return kameitenMasterDA.SelKameitenInfoIttukatuErrorCsv(strEdidate, strSyoridate)
    End Function

    ''' <summary>
    ''' �X�V�����`�F�b�N
    ''' </summary>
    ''' <param name="strLine"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2012/05/08 �ԗ� 407553�̑Ή� �ǉ�</history>
    Private Function ChkUpdDate(ByRef strLine As String) As Boolean
        Dim arrLine() As String = Split(strLine, ",")

        '�����X�R�[�h
        Dim strKameitenCd As String = arrLine(2).Trim
        '�����X�̍X�V����
        Dim strKameitenUpdDate As String = arrLine(91).Trim
        '�����X�Z���̍X�V����
        Dim strKameitenJyousyoUpdDate As String = arrLine(95).Trim
        '���������ݒ�1�̍X�V����
        Dim strTatouwaribikiSettei1UpdDate As String = arrLine(92).Trim
        '���������ݒ�2�̍X�V����
        Dim strTatouwaribikiSettei2UpdDate As String = arrLine(93).Trim
        '���������ݒ�3�̍X�V����
        Dim strTatouwaribikiSettei3UpdDate As String = arrLine(94).Trim

        '�e�}�X�^�̍X�V����
        Dim dtMstUpdDate As New Data.DataTable
        Dim strMstUpdDate As String = String.Empty

        '�����X�̍X�V�����`�F�b�N
        If Not strKameitenUpdDate.Equals(String.Empty) Then
            'If Not IsDate(strKameitenUpdDate) Then
            '    Return False
            'Else
            '    '�����X�}�X�^�̍X�V�������擾����
            '    dtMstUpdDate = kameitenMasterDA.SelKameitenUpdDate(strKameitenCd)
            '    If dtMstUpdDate.Rows.Count > 0 Then
            '        strMstUpdDate = dtMstUpdDate.Rows(0).Item(0).ToString.Trim
            '    Else
            '        strMstUpdDate = String.Empty
            '    End If
            '    If Not strMstUpdDate.Equals(String.Empty) Then
            '        'CSV.�X�V�����ƃ}�X�^.�X�V�������r����
            '        If Date.Compare(Convert.ToDateTime(strKameitenUpdDate), Convert.ToDateTime(strMstUpdDate)) < 0 Then
            '            'CSV.�X�V���� < �}�X�^.�X�V���� �̏ꍇ
            '            Return False
            '        End If
            '    End If
            'End If

            '�����X�}�X�^�̍X�V�������擾����
            dtMstUpdDate = kameitenMasterDA.SelKameitenUpdDate(strKameitenCd)
            If dtMstUpdDate.Rows.Count > 0 Then
                strMstUpdDate = dtMstUpdDate.Rows(0).Item(0).ToString.Trim
            Else
                strMstUpdDate = String.Empty
            End If
            'CSV.�X�V�����ƃ}�X�^.�X�V�������r����
            If String.Compare(strKameitenUpdDate, strMstUpdDate) < 0 Then
                'CSV.�X�V���� < �}�X�^.�X�V���� �̏ꍇ
                Return False
            End If
        End If

        '�����X�Z���̍X�V�����`�F�b�N
        If Not strKameitenJyousyoUpdDate.Equals(String.Empty) Then
            'If Not IsDate(strKameitenJyousyoUpdDate) Then
            '    Return False
            'Else
            '    '�����X�Z���}�X�^�̍X�V�������擾����
            '    dtMstUpdDate = kameitenMasterDA.SelKameitenJyuusyoUpdDate(strKameitenCd)
            '    If dtMstUpdDate.Rows.Count > 0 Then
            '        strMstUpdDate = dtMstUpdDate.Rows(0).Item(0).ToString.Trim
            '    Else
            '        strMstUpdDate = String.Empty
            '    End If
            '    If Not strMstUpdDate.Equals(String.Empty) Then
            '        'CSV.�X�V�����ƃ}�X�^.�X�V�������r����
            '        If Date.Compare(Convert.ToDateTime(strKameitenJyousyoUpdDate), Convert.ToDateTime(strMstUpdDate)) < 0 Then
            '            'CSV.�X�V���� < �}�X�^.�X�V���� �̏ꍇ
            '            Return False
            '        End If
            '    End If
            'End If

            '�����X�Z���}�X�^�̍X�V�������擾����
            dtMstUpdDate = kameitenMasterDA.SelKameitenJyuusyoUpdDate(strKameitenCd)
            If dtMstUpdDate.Rows.Count > 0 Then
                strMstUpdDate = dtMstUpdDate.Rows(0).Item(0).ToString.Trim
            Else
                strMstUpdDate = String.Empty
            End If
            'CSV.�X�V�����ƃ}�X�^.�X�V�������r����
            If String.Compare(strKameitenJyousyoUpdDate, strMstUpdDate) < 0 Then
                'CSV.�X�V���� < �}�X�^.�X�V���� �̏ꍇ
                Return False
            End If
        End If

        '���������ݒ�1�̍X�V�����`�F�b�N
        If Not strTatouwaribikiSettei1UpdDate.Equals(String.Empty) Then
            'If Not IsDate(strTatouwaribikiSettei1UpdDate) Then
            '    Return False
            'Else
            '    '���������ݒ�}�X�^�̍X�V�������擾����
            '    dtMstUpdDate = kameitenMasterDA.SelTatouwaribikiSetteiUpdDate(strKameitenCd, "1")
            '    If dtMstUpdDate.Rows.Count > 0 Then
            '        strMstUpdDate = dtMstUpdDate.Rows(0).Item(0).ToString.Trim
            '    Else
            '        strMstUpdDate = String.Empty
            '    End If
            '    If Not strMstUpdDate.Equals(String.Empty) Then
            '        'CSV.�X�V�����ƃ}�X�^.�X�V�������r����
            '        If Date.Compare(Convert.ToDateTime(strTatouwaribikiSettei1UpdDate), Convert.ToDateTime(strMstUpdDate)) < 0 Then
            '            'CSV.�X�V���� < �}�X�^.�X�V���� �̏ꍇ
            '            Return False
            '        End If
            '    End If
            'End If

            '���������ݒ�}�X�^�̍X�V�������擾����
            dtMstUpdDate = kameitenMasterDA.SelTatouwaribikiSetteiUpdDate(strKameitenCd, "1")
            If dtMstUpdDate.Rows.Count > 0 Then
                strMstUpdDate = dtMstUpdDate.Rows(0).Item(0).ToString.Trim
            Else
                strMstUpdDate = String.Empty
            End If
            'CSV.�X�V�����ƃ}�X�^.�X�V�������r����
            If String.Compare(strTatouwaribikiSettei1UpdDate, strMstUpdDate) < 0 Then
                'CSV.�X�V���� < �}�X�^.�X�V���� �̏ꍇ
                Return False
            End If
        End If

        '���������ݒ�2�̍X�V�����`�F�b�N
        If Not strTatouwaribikiSettei2UpdDate.Equals(String.Empty) Then
            'If Not IsDate(strTatouwaribikiSettei2UpdDate) Then
            '    Return False
            'Else
            '    '���������ݒ�}�X�^�̍X�V�������擾����
            '    dtMstUpdDate = kameitenMasterDA.SelTatouwaribikiSetteiUpdDate(strKameitenCd, "2")
            '    If dtMstUpdDate.Rows.Count > 0 Then
            '        strMstUpdDate = dtMstUpdDate.Rows(0).Item(0).ToString.Trim
            '    Else
            '        strMstUpdDate = String.Empty
            '    End If
            '    If Not strMstUpdDate.Equals(String.Empty) Then
            '        'CSV.�X�V�����ƃ}�X�^.�X�V�������r����
            '        If Date.Compare(Convert.ToDateTime(strTatouwaribikiSettei2UpdDate), Convert.ToDateTime(strMstUpdDate)) < 0 Then
            '            'CSV.�X�V���� < �}�X�^.�X�V���� �̏ꍇ
            '            Return False
            '        End If
            '    End If
            'End If

            '���������ݒ�}�X�^�̍X�V�������擾����
            dtMstUpdDate = kameitenMasterDA.SelTatouwaribikiSetteiUpdDate(strKameitenCd, "2")
            If dtMstUpdDate.Rows.Count > 0 Then
                strMstUpdDate = dtMstUpdDate.Rows(0).Item(0).ToString.Trim
            Else
                strMstUpdDate = String.Empty
            End If
            'CSV.�X�V�����ƃ}�X�^.�X�V�������r����
            If String.Compare(strTatouwaribikiSettei2UpdDate, strMstUpdDate) < 0 Then
                'CSV.�X�V���� < �}�X�^.�X�V���� �̏ꍇ
                Return False
            End If
        End If

        '���������ݒ�3�̍X�V�����`�F�b�N
        If Not strTatouwaribikiSettei3UpdDate.Equals(String.Empty) Then
            'If Not IsDate(strTatouwaribikiSettei3UpdDate) Then
            '    Return False
            'Else
            '    '���������ݒ�}�X�^�̍X�V�������擾����
            '    dtMstUpdDate = kameitenMasterDA.SelTatouwaribikiSetteiUpdDate(strKameitenCd, "3")
            '    If dtMstUpdDate.Rows.Count > 0 Then
            '        strMstUpdDate = dtMstUpdDate.Rows(0).Item(0).ToString.Trim
            '    Else
            '        strMstUpdDate = String.Empty
            '    End If
            '    If Not strMstUpdDate.Equals(String.Empty) Then
            '        'CSV.�X�V�����ƃ}�X�^.�X�V�������r����
            '        If Date.Compare(Convert.ToDateTime(strTatouwaribikiSettei3UpdDate), Convert.ToDateTime(strMstUpdDate)) < 0 Then
            '            'CSV.�X�V���� < �}�X�^.�X�V���� �̏ꍇ
            '            Return False
            '        End If
            '    End If
            'End If

            '���������ݒ�}�X�^�̍X�V�������擾����
            dtMstUpdDate = kameitenMasterDA.SelTatouwaribikiSetteiUpdDate(strKameitenCd, "3")
            If dtMstUpdDate.Rows.Count > 0 Then
                strMstUpdDate = dtMstUpdDate.Rows(0).Item(0).ToString.Trim
            Else
                strMstUpdDate = String.Empty
            End If
            'CSV.�X�V�����ƃ}�X�^.�X�V�������r����
            If String.Compare(strTatouwaribikiSettei3UpdDate, strMstUpdDate) < 0 Then
                'CSV.�X�V���� < �}�X�^.�X�V���� �̏ꍇ
                Return False
            End If
        End If

        Return True
    End Function

End Class