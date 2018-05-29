Imports System.Transactions
Imports Itis.Earth.DataAccess

''' <summary>�����}�X�^</summary>
''' <remarks>�����}�X�^�p�@�\��񋟂���</remarks>
''' <history>
''' <para>2011/02/24�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</para>
''' </history>
Public Class GenkaMasterLogic

    Private genkaMasterDA As New GenkaMasterDataAccess

    Private commonCheck As New CsvInputCheck
    Private ninsyou As New Ninsyou()
    Private userId As String = Ninsyou.GetUserID()
    Private InputDate As String
    'CSV�A�b�v���[�h�������
    Private CsvInputMaxLineCount As String = CStr(System.Configuration.ConfigurationSettings.AppSettings("CsvInputMaxLineCount"))


    ''' <summary>������Ж����擾����</summary>
    ''' <returns>������Ж��f�[�^�e�[�u��</returns>
    ''' <history>2011/03/07�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetTyousaKaisyaMei(ByVal strTyousaKaisyaCd As String, ByVal strJigyousyoCd As String, ByVal strKensakuTaisyouGai As String) As Data.DataTable

        Return genkaMasterDA.SelTyousaKaisyaMei(strTyousaKaisyaCd, strJigyousyoCd, strKensakuTaisyouGai)

    End Function

    ''' <summary>������ʂ��擾����</summary>
    ''' <returns>������ʃf�[�^�e�[�u��</returns>
    ''' <history>2011/02/24�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetAiteSakiSyubetu() As Data.DataTable

        Return genkaMasterDA.SelAiteSakiSyubetu()

    End Function

    ''' <summary>����於���擾����</summary>
    ''' <returns>����於�f�[�^�e�[�u��</returns>
    ''' <history>2011/03/07�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetAitesakiMei(ByVal intAitesakiSyubetu As Integer, ByVal AitesakiCd As String, ByVal strTorikesiAitesaki As String) As Data.DataTable

        Return genkaMasterDA.SelAitesakiMei(intAitesakiSyubetu, AitesakiCd, strTorikesiAitesaki)

    End Function

    ''' <summary>���i�R�[�h���擾����</summary>
    ''' <returns>���i�R�[�h�f�[�^�e�[�u��</returns>
    ''' <history>2011/02/24�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetSyouhinCd() As Data.DataTable

        Return genkaMasterDA.SelSyouhinCd()

    End Function

    ''' <summary>�������@���擾����</summary>
    ''' <returns>�������@�f�[�^�e�[�u��</returns>
    ''' <history>2011/02/24�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetTyousaHouhou() As Data.DataTable

        Return genkaMasterDA.SelTyousaHouhou()

    End Function

    ''' <summary>���������擾����</summary>
    ''' <returns>�������f�[�^�e�[�u��</returns>
    ''' <history>2011/02/25�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetGenkaJyouhou(ByVal strKensakuCount As String, ByVal strTysKaisyaCd As String, ByVal strAtesakiSyubetu As String, ByVal strAtesakiCdFrom As String, ByVal strAtesakiCdTo As String, ByVal strSyouhinCd As String, ByVal strHouhouCd As String, ByVal blnKensakuTaisyouGai As Boolean, ByVal blnAitesakiTaisyouGai As Boolean) As GenkaMasterDataSet.GenkaInfoTableDataTable

        Return genkaMasterDA.SelGenkaJyouhou(strKensakuCount, strTysKaisyaCd, strAtesakiSyubetu, strAtesakiCdFrom, strAtesakiCdTo, strSyouhinCd, strHouhouCd, blnKensakuTaisyouGai, blnAitesakiTaisyouGai)

    End Function

    ''' <summary>������񌏐����擾����</summary>
    ''' <returns>�������f�[�^�e�[�u��</returns>
    ''' <history>2011/02/25�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetGenkaJyouhouCount(ByVal strTysKaisyaCd As String, ByVal strAtesakiSyubetu As String, ByVal strAtesakiCdFrom As String, ByVal strAtesakiCdTo As String, ByVal strSyouhinCd As String, ByVal strHouhouCd As String, ByVal blnKensakuTaisyouGai As Boolean, ByVal blnAitesakiTaisyouGai As Boolean) As Integer

        Return genkaMasterDA.SelGenkaJyouhouCount(strTysKaisyaCd, strAtesakiSyubetu, strAtesakiCdFrom, strAtesakiCdTo, strSyouhinCd, strHouhouCd, blnKensakuTaisyouGai, blnAitesakiTaisyouGai)

    End Function

    ''' <summary>�����X�������擾����</summary>
    ''' <returns>�����X����</returns>
    Public Function GetKameitenCount(ByVal strAitesakiCdFrom As String, ByVal strAitesakiCdTo As String, ByVal strTorikesiAitesaki As String) As Integer
        Return genkaMasterDA.SelKameitenCount(strAitesakiCdFrom, strAitesakiCdTo, strTorikesiAitesaki)
    End Function


    ''' <summary>�������CSV���擾����</summary>
    ''' <returns>�������CSV�f�[�^�e�[�u��</returns>
    ''' <history>2011/02/28�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetGenkaJyouhouCSV(ByVal strTysKaisyaCd As String, ByVal strAtesakiSyubetu As String, ByVal strAtesakiCdFrom As String, ByVal strAtesakiCdTo As String, ByVal strSyouhinCd As String, ByVal strHouhouCd As String, ByVal blnKensakuTaisyouGai As Boolean, ByVal blnAitesakiTaisyouGai As Boolean) As Data.DataTable
        Return genkaMasterDA.SelGenkaJyouhouCSV(strTysKaisyaCd, strAtesakiSyubetu, strAtesakiCdFrom, strAtesakiCdTo, strSyouhinCd, strHouhouCd, blnKensakuTaisyouGai, blnAitesakiTaisyouGai)
    End Function


    ''' <summary>���ݒ���܂ތ���CSV�������擾����</summary>
    ''' <returns>���ݒ���܂ތ���CSV����</returns>
    Public Function GetMiSeteiGenkaCSVCount(ByVal strTysKaisyaCd As String, ByVal strAtesakiSyubetu As String, ByVal strAtesakiCdFrom As String, ByVal strAtesakiCdTo As String, ByVal strSyouhinCd As String, ByVal strHouhouCd As String, ByVal blnKensakuTaisyouGai As Boolean, ByVal blnAitesakiTaisyouGai As Boolean) As Long
        Return genkaMasterDA.SelMiSeteiGenkaCSVCount(strTysKaisyaCd, strAtesakiSyubetu, strAtesakiCdFrom, strAtesakiCdTo, strSyouhinCd, strHouhouCd, blnKensakuTaisyouGai, blnAitesakiTaisyouGai)
    End Function

    ''' <summary>���ݒ���܂ތ���CSV�f�[�^���擾����</summary>
    ''' <returns>���ݒ���܂ތ���CSV�f�[�^�e�[�u��</returns>
    Public Function GetMiSeteiGenkaCSVInfo(ByVal strTysKaisyaCd As String, ByVal strAtesakiSyubetu As String, ByVal strAtesakiCdFrom As String, ByVal strAtesakiCdTo As String, ByVal strSyouhinCd As String, ByVal strHouhouCd As String, ByVal blnKensakuTaisyouGai As Boolean, ByVal blnAitesakiTaisyouGai As Boolean) As Data.DataTable
        Return genkaMasterDA.SelMiSeteiGenkaCSVInfo(strTysKaisyaCd, strAtesakiSyubetu, strAtesakiCdFrom, strAtesakiCdTo, strSyouhinCd, strHouhouCd, blnKensakuTaisyouGai, blnAitesakiTaisyouGai)
    End Function

    ''' <summary>�A�b�v���[�h�Ǘ����擾����</summary>
    ''' <returns>�A�b�v���[�h�Ǘ��f�[�^�e�[�u��</returns>
    ''' <history>2011/03/01�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetInputKanri() As Data.DataTable

        Return genkaMasterDA.SelInputKanri()

    End Function

    ''' <summary>�A�b�v���[�h�Ǘ��̌������擾����</summary>
    ''' <returns>�A�b�v���[�h�Ǘ��̌����f�[�^�e�[�u��</returns>
    ''' <history>2011/03/01�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetInputKanriCount() As Integer

        Return genkaMasterDA.SelInputKanriCount()

    End Function

    ''' <summary>CSV�t�@�C�����`�F�b�N����</summary>
    ''' <returns>CSV�t�@�C�����`�F�b�N����</returns>
    ''' <history>2011/03/01�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function ChkCsvFile(ByVal fupId As Web.UI.WebControls.FileUpload, ByRef strUmuFlg As String) As String

        Dim myStream As IO.Stream                           '���o�̓X�g���[��
        Dim myReader As IO.StreamReader                     '�X�g���[�����[�_�[
        Dim strReadLine As String                           '�捞�t�@�C���Ǎ��ݍs
        Dim intLineCount As Integer = 0                     '���C����
        Dim strCsvLine() As String                          'CSV�t�@�C�����e
        Dim strFileMei As String                            'CSV�t�@�C����
        Dim strEdiJouhouSakuseiDate As String = String.Empty 'EDI���쐬��
        Dim dtOk As New Data.DataTable                      '�����}�X�^
        Dim dtError As New Data.DataTable                   '�����G���[

        '�����}�X�^���쐬
        Call Me.CreateOkDataTable(dtOk)
        '�����G���[�e�[�u�����쐬
        Call Me.CreateErrorDataTable(dtError)
        '�V�X�e��Date
        InputDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")
        'CSV�t�@�C����
        strFileMei = commonCheck.CutMaxLength(fupId.FileName, 128)
        '���o�̓X�g���[��
        myStream = fupId.FileContent
        '�X�g���[�����[�_�[
        myReader = New IO.StreamReader(myStream, System.Text.Encoding.GetEncoding(932))

        Try
            Do
                '�捞�t�@�C����ǂݍ���
                ReDim Preserve strCsvLine(intLineCount)
                strCsvLine(intLineCount) = myReader.ReadLine()
                intLineCount += 1
            Loop Until myReader.EndOfStream

            'CSV�A�b�v���[�h��������`�F�b�N
            For i As Integer = strCsvLine.Length - 1 To 0 Step -1
                If Not strCsvLine(i).Trim.Equals(String.Empty) Then
                    If i > CInt(CsvInputMaxLineCount) Then
                        Return String.Format(Messages.Instance.MSG040E, CStr(CsvInputMaxLineCount))
                    ElseIf i < 1 Then
                        Return String.Format(Messages.Instance.MSG048E)
                    Else
                        Exit For
                    End If
                End If
            Next

            'EDI���쐬��
            strEdiJouhouSakuseiDate = Right("0" & strCsvLine(1).Split(",")(0).Trim, 10)

            'CSV�t�@�C�����`�F�b�N
            For i As Integer = 1 To strCsvLine.Length - 1
                strReadLine = strCsvLine(i)
                If (Not strReadLine Is Nothing) AndAlso (Replace(strReadLine, ",", "") <> "") Then
                    Dim arrLine() As String = strReadLine.Split(",")
                    If strReadLine.Split(",")(0).Length < 10 Then
                        strReadLine = "0" & strReadLine
                    End If
                    '�����i��ʁE�R�[�h)
                    If arrLine(4) = "3" Then
                        arrLine(5) = "JIO"
                    End If
                    strReadLine = String.Join(",", arrLine)
                    '�J���}��ǉ�
                    If strReadLine.Split(",").Length < CsvInputCheck.GENKA_FIELD_COUNT Then
                        strReadLine = strReadLine & StrDup(CsvInputCheck.GENKA_FIELD_COUNT - strReadLine.Split(",").Length, ",")
                    End If

                    '�t�B�[���h���`�F�b�N
                    If Not commonCheck.ChkFieldCount(strReadLine.Split(",").Length, CsvInputCheck.GENKA) Then
                        '�G���[�f�[�^�̏���
                        Call Me.ErrorLineSyori(i + 1, strReadLine, dtError)
                        Continue For
                    End If

                    '���ڍő咷�`�F�b�N
                    If Not commonCheck.ChkMaxLength(strReadLine, CsvInputCheck.GENKA) Then
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
                    If Not commonCheck.ChkSuuti(strReadLine, CsvInputCheck.GENKA) Then
                        '�G���[�f�[�^�̏���
                        Call Me.ErrorLineSyori(i + 1, strReadLine, dtError)
                        Continue For
                    End If

                    '�K�{�`�F�b�N
                    If Not commonCheck.ChkNotNull(strReadLine, CsvInputCheck.GENKA) Then
                        '�G���[�f�[�^�̏���
                        Call Me.ErrorLineSyori(i + 1, strReadLine, dtError)
                        Continue For
                    End If

                    '�O0����n���ϊ�
                    strReadLine = SetUmekusa(strReadLine)

                    '���݃`�F�b�N
                    If Not Me.ChkSonZai(strReadLine) Then
                        '�G���[�f�[�^�̏���
                        Call Me.ErrorLineSyori(i + 1, strReadLine, dtError)
                        Continue For
                    End If

                    '�X�V�����`�F�b�N
                    If Not commonCheck.ChkKousinJyouken(strReadLine, CsvInputCheck.GENKA) Then

                        Continue For
                    End If

                    '���i�f�[�^�̏���
                    Call Me.OkLineSyori(strReadLine, dtOk)

                End If
            Next

            '�G���[�L���t���O��ݒ肷��
            If dtError.Rows.Count > 0 Then
                strUmuFlg = "1"
            End If

            'CSV�t�@�C�����捞
            If Not CsvFileInput(dtOk, dtError, InputDate, userId, strFileMei, strEdiJouhouSakuseiDate) Then
                Return Messages.Instance.MSG050E
            End If

        Catch ex As Exception
            Return Messages.Instance.MSG049E
        End Try

        Return String.Empty

    End Function

    ''' <summary>
    ''' �O0����n���ϊ�
    ''' </summary>
    Public Function SetUmekusa(ByVal StrLine As String) As String

        Dim arrLine() As String = StrLine.Split(",")

        '������ЃR�[�h
        If arrLine(1).ToString <> String.Empty Then
            arrLine(1) = Right("0000" & arrLine(1).Trim, 4)
        End If

        '���Ə��R�[�h
        If arrLine(2).ToString <> String.Empty Then
            arrLine(2) = Right("00" & arrLine(2).Trim, 2)
        End If

        '�����i��ʁE�R�[�h)
        Select Case arrLine(4)
            Case "1"
                arrLine(5) = Right("00000" & arrLine(5).Trim, 5)
            Case "7"
                If arrLine(5).Length < 4 Then
                    arrLine(5) = Right("0000" & arrLine(5).Trim, 4)
                End If
        End Select

        Return String.Join(",", arrLine)

    End Function

    ''' <summary>CSV�t�@�C�����捞����</summary>
    ''' <returns>CSV�t�@�C�����捞����</returns>
    ''' <history>2011/03/01�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function CsvFileInput(ByVal dtOk As Data.DataTable, ByVal dtError As Data.DataTable, ByVal InputDate As String, ByVal userId As String, ByVal strFileMei As String, ByVal strEdiJouhouSakuseiDate As String) As Boolean

        Using scope As New TransactionScope(TransactionScopeOption.RequiresNew)
            Try
                '�����}�X�^��o�^
                If dtOk.Rows.Count > 0 Then
                    If Not genkaMasterDA.InsUpdGenkaMaster(dtOk) Then
                        Throw New ApplicationException
                    End If

                End If

                '�����G���[���e�[�u����o�^
                If dtError.Rows.Count > 0 Then
                    If Not genkaMasterDA.InsGenkaError(dtError) Then
                        Throw New ApplicationException
                    End If
                End If

                '�A�b�v���[�h�Ǘ��e�[�u����o�^
                If Not genkaMasterDA.InsInputKanri(InputDate, strFileMei, strEdiJouhouSakuseiDate, CInt(IIf(dtError.Rows.Count, 1, 0)), userId) Then
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


    ''' <summary>���݃`�F�b�N</summary>
    Private Function ChkSonZai(ByVal intFieldCount As String) As Boolean

        '������Ёi������ЃR�[�h�E���Ə��R�[�h�j
        If Not genkaMasterDA.SelTyousaKaisyaMeiInput(intFieldCount.Split(",")(1).Trim, intFieldCount.Split(",")(2).Trim) Then
            Return False
        End If

        '�����(��ʁE�R�[�h)
        If Not genkaMasterDA.SelAitesakiSyubetuInput(CInt(intFieldCount.Split(",")(4)), intFieldCount.Split(",")(5).Trim) Then
            Return False
        End If

        '���i�R�[�h
        If Not genkaMasterDA.SelSyouhinCdInput(intFieldCount.Split(",")(7).Trim) Then
            Return False
        End If

        '�������@NO
        If Not genkaMasterDA.SelTyousahouhouNoInput(intFieldCount.Split(",")(9).Trim) Then
            Return False
        End If

        Return True
    End Function


    ''' <summary>�G���[���C������</summary>
    Private Sub ErrorLineSyori(ByVal intLineNo As Integer, ByVal strLine As String, ByRef dtError As Data.DataTable)

        Dim intMaxCount As Integer

        Dim dr As Data.DataRow
        dr = dtError.NewRow


        If strLine.Split(",").Length < CsvInputCheck.GENKA_FIELD_COUNT Then
            intMaxCount = strLine.Split(",").Length
        Else
            intMaxCount = CsvInputCheck.GENKA_FIELD_COUNT
        End If

        For i As Integer = 0 To intMaxCount - 1
            '�ő咷��؂���
            dr.Item(i) = commonCheck.CutMaxLength(strLine.Split(",")(i).Trim, commonCheck.GENKA_MAX_LENGTH(i))
        Next


        '�s��
        dr.Item(CsvInputCheck.GENKA_FIELD_COUNT) = CStr(intLineNo)
        '��������
        dr.Item(CsvInputCheck.GENKA_FIELD_COUNT + 1) = InputDate
        '�o�^���O�C�����[�UID
        dr.Item(CsvInputCheck.GENKA_FIELD_COUNT + 2) = userId

        dtError.Rows.Add(dr)


    End Sub


    ''' <summary>OK���C������</summary>
    Private Sub OkLineSyori(ByVal strLine As String, ByRef dtOk As Data.DataTable)

        'DB���݃`�F�b�N
        If genkaMasterDA.SelGenkaInputJyouhou(strLine.Split(",")(1).Trim, strLine.Split(",")(2).Trim, strLine.Split(",")(4).Trim, strLine.Split(",")(5).Trim, strLine.Split(",")(7).Trim, strLine.Split(",")(9).Trim) Then
            Call Me.SetOkDataRow(strLine, dtOk, "1")
        Else
            If dtOk.Rows.Count > 0 Then
                '���݃t���O
                Dim SonzaiFlg As Boolean = False

                For i As Integer = 0 To dtOk.Rows.Count - 1
                    If dtOk.Rows(i).Item("key").ToString.Trim.Equals(GetDataKey(strLine)) Then
                        SonzaiFlg = True
                        Exit For
                    End If
                Next

                If SonzaiFlg.Equals(True) Then
                    Call Me.SetOkDataRow(strLine, dtOk, "1")
                Else
                    Call Me.SetOkDataRow(strLine, dtOk, "0")
                End If
            Else
                Call Me.SetOkDataRow(strLine, dtOk, "0")
            End If

        End If



    End Sub

    ''' <summary>OK�f�[�^���C����ǉ�</summary>
    Public Sub SetOkDataRow(ByVal strLine As String, ByRef dt As Data.DataTable, ByVal strInsUpdFlg As String)

        Dim dr As Data.DataRow
        dr = dt.NewRow
        dr.Item("key") = GetDataKey(strLine)
        dr.Item("ins_upd_flg") = strInsUpdFlg
        dr.Item("tys_kaisya_cd") = strLine.Split(",")(1).Trim
        dr.Item("jigyousyo_cd") = strLine.Split(",")(2).Trim
        dr.Item("aitesaki_syubetu") = strLine.Split(",")(4).Trim
        dr.Item("aitesaki_cd") = strLine.Split(",")(5).Trim
        dr.Item("syouhin_cd") = strLine.Split(",")(7).Trim
        dr.Item("tys_houhou_no") = strLine.Split(",")(9).Trim
        dr.Item("torikesi") = IIf(strLine.Split(",")(11).Trim.Equals(String.Empty), "0", strLine.Split(",")(11).Trim)
        dr.Item("tou_kkk1") = strLine.Split(",")(12).Trim
        dr.Item("tou_kkk_henkou_flg1") = strLine.Split(",")(13).Trim
        dr.Item("tou_kkk2") = strLine.Split(",")(14).Trim
        dr.Item("tou_kkk_henkou_flg2") = strLine.Split(",")(15).Trim
        dr.Item("tou_kkk3") = strLine.Split(",")(16).Trim
        dr.Item("tou_kkk_henkou_flg3") = strLine.Split(",")(17).Trim
        dr.Item("tou_kkk4") = strLine.Split(",")(18).Trim
        dr.Item("tou_kkk_henkou_flg4") = strLine.Split(",")(19).Trim
        dr.Item("tou_kkk5") = strLine.Split(",")(20).Trim
        dr.Item("tou_kkk_henkou_flg5") = strLine.Split(",")(21).Trim
        dr.Item("tou_kkk6") = strLine.Split(",")(22).Trim
        dr.Item("tou_kkk_henkou_flg6") = strLine.Split(",")(23).Trim
        dr.Item("tou_kkk7") = strLine.Split(",")(24).Trim
        dr.Item("tou_kkk_henkou_flg7") = strLine.Split(",")(25).Trim
        dr.Item("tou_kkk8") = strLine.Split(",")(26).Trim
        dr.Item("tou_kkk_henkou_flg8") = strLine.Split(",")(27).Trim
        dr.Item("tou_kkk9") = strLine.Split(",")(28).Trim
        dr.Item("tou_kkk_henkou_flg9") = strLine.Split(",")(29).Trim
        dr.Item("tou_kkk10") = strLine.Split(",")(30).Trim
        dr.Item("tou_kkk_henkou_flg10") = strLine.Split(",")(31).Trim
        dr.Item("tou_kkk11t19") = strLine.Split(",")(32).Trim
        dr.Item("tou_kkk_henkou_flg11t19") = strLine.Split(",")(33).Trim
        dr.Item("tou_kkk20t29") = strLine.Split(",")(34).Trim
        dr.Item("tou_kkk_henkou_flg20t29") = strLine.Split(",")(35).Trim
        dr.Item("tou_kkk30t39") = strLine.Split(",")(36).Trim
        dr.Item("tou_kkk_henkou_flg30t39") = strLine.Split(",")(37).Trim
        dr.Item("tou_kkk40t49") = strLine.Split(",")(38).Trim
        dr.Item("tou_kkk_henkou_flg40t49") = strLine.Split(",")(39).Trim
        dr.Item("tou_kkk50t") = strLine.Split(",")(40).Trim
        dr.Item("tou_kkk_henkou_flg50t") = strLine.Split(",")(41).Trim
        dr.Item("add_login_user_id") = userId
        dr.Item("upd_login_user_id") = userId

        dt.Rows.Add(dr)

    End Sub

    ''' <summary>OK�f�[�^��key��ݒ�</summary>
    Public Function GetDataKey(ByVal strLine As String) As String

        Return strLine.Split(",")(1).Trim & "," & strLine.Split(",")(2).Trim & "," & strLine.Split(",")(4).Trim & "," & strLine.Split(",")(5).Trim & "," & strLine.Split(",")(7).Trim & "," & strLine.Split(",")(9).Trim

    End Function


    ''' <summary>�����}�X�^���쐬</summary>
    Public Sub CreateOkDataTable(ByRef dt As Data.DataTable)

        Dim dc As Data.DataColumn

        dc = New Data.DataColumn
        dc.ColumnName = "key" '�L�[(������ЃR�[�h + ���Ə��R�[�h + ������� + �����R�[�h + ���i�R�[�h + �������@NO)
        dt.Columns.Add(dc)

        dc = New Data.DataColumn
        dc.ColumnName = "ins_upd_flg" '�ǉ��X�VFLG(0:�ǉ�; 1:�X�V)
        dt.Columns.Add(dc)

        dc = New Data.DataColumn
        dc.ColumnName = "tys_kaisya_cd" '������ЃR�[�h
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "jigyousyo_cd"  '���Ə��R�[�h
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "aitesaki_syubetu"  '�������
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "aitesaki_cd"   '�����R�[�h
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "syouhin_cd"    '���i�R�[�h
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tys_houhou_no" '�������@NO
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "torikesi"  '���
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk1"  '�����i1
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg1"   '�����i�ύXFLG1
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk2"  '�����i2
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg2"   '�����i�ύXFLG2
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk3"  '�����i3
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg3"   '�����i�ύXFLG3
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk4"  '�����i4
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg4"   '�����i�ύXFLG4
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk5"  '�����i5
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg5"   '�����i�ύXFLG5
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk6"  '�����i6
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg6"   '�����i�ύXFLG6
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk7"  '�����i7
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg7"   '�����i�ύXFLG7
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk8"  '�����i8
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg8"   '�����i�ύXFLG8
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk9"  '�����i9
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg9"   '�����i�ύXFLG9
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk10" '�����i10
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg10"  '�����i�ύXFLG10
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk11t19"  '�����i11�`19
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg11t19"   '�����i�ύXFLG11�`19
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk20t29"  '�����i20�`29
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg20t29"   '�����i�ύXFLG20�`29
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk30t39"  '�����i30�`39
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg30t39"   '�����i�ύXFLG30�`39
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk40t49"  '�����i40�`49
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg40t49"   '�����i�ύXFLG40�`49
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk50t"    '�����i50�`
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg50t" '�����i�ύXFLG50�`
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "add_login_user_id" '�o�^���O�C�����[�UID
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "add_datetime"  '�o�^����
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "upd_login_user_id" '�X�V���O�C�����[�UID
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "upd_datetime"  '�X�V����
        dt.Columns.Add(dc)

    End Sub

    ''' <summary>�����G���[�e�[�u�����쐬</summary>
    Public Sub CreateErrorDataTable(ByRef dt As Data.DataTable)

        Dim dc As Data.DataColumn

        dc = New Data.DataColumn
        dc.ColumnName = "edi_jouhou_sakusei_date"   'EDI���쐬��
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tys_kaisya_cd" '������ЃR�[�h
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "jigyousyo_cd"  '���Ə��R�[�h
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tys_kaisya_mei"    '������Ж�
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "aitesaki_syubetu"  '�������
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "aitesaki_cd"   '�����R�[�h
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "aitesaki_mei"  '����於
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "syouhin_cd"    '���i�R�[�h
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "syouhin_mei"   '���i��
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tys_houhou_no" '�������@NO
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tys_houhou"    '�������@
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "torikesi"  '���
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk1"  '�����i1
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg1"   '�����i�ύXFLG1
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk2"  '�����i2
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg2"   '�����i�ύXFLG2
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk3"  '�����i3
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg3"   '�����i�ύXFLG3
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk4"  '�����i4
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg4"   '�����i�ύXFLG4
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk5"  '�����i5
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg5"   '�����i�ύXFLG5
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk6"  '�����i6
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg6"   '�����i�ύXFLG6
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk7"  '�����i7
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg7"   '�����i�ύXFLG7
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk8"  '�����i8
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg8"   '�����i�ύXFLG8
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk9"  '�����i9
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg9"   '�����i�ύXFLG9
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk10" '�����i10
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg10"  '�����i�ύXFLG10
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk11t19"  '�����i11�`19
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg11t19"   '�����i�ύXFLG11�`19
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk20t29"  '�����i20�`29
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg20t29"   '�����i�ύXFLG20�`29
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk30t39"  '�����i30�`39
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg30t39"   '�����i�ύXFLG30�`39
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk40t49"  '�����i40�`49
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg40t49"   '�����i�ύXFLG40�`49
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk50t"    '�����i50�`
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tou_kkk_henkou_flg50t" '�����i�ύXFLG50�`
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "gyou_no"   '�sNO
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "syori_datetime"    '��������
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "add_login_user_id" '�o�^���O�C�����[�UID
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "add_datetime"  '�o�^����
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "upd_login_user_id" '�X�V���O�C�����[�UID
        dt.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "upd_datetime"  '�X�V����
        dt.Columns.Add(dc)

    End Sub

    ''' <summary>�����G���[�����擾����</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI���쐬��</param>
    ''' <returns>�����G���[�f�[�^�e�[�u��</returns>
    Public Function GetGenkaErr(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoridate As String) As Data.DataTable
        Return genkaMasterDA.SelGenkaErr(strEdiJouhouSakuseiDate, strSyoridate)
    End Function

    ''' <summary>�����G���[�������擾����</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI���쐬��</param>
    ''' <returns>�����G���[����</returns>
    Public Function GetGenkaErrCount(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoridate As String) As String
        Return genkaMasterDA.SelGenkaErrCount(strEdiJouhouSakuseiDate, strSyoridate)
    End Function

    ''' <summary>�����G���[CSV�����擾����</summary>
    ''' <returns>�����G���[CSV�f�[�^�e�[�u��</returns>
    Public Function SelGenkaErrCsv(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoridate As String) As Data.DataTable
        Return genkaMasterDA.SelGenkaErrCsv(strEdiJouhouSakuseiDate, strSyoridate)
    End Function

End Class
