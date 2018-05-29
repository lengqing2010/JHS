Imports Itis.Earth.DataAccess
Imports System.Transactions

Public Class TokubetuTaiouMasterLogic

    Private tokubetuTaiouMasterDA As New TokubetuTaiouMasterDataAccess
    Private commonCheck As New CsvInputCheck
    Private ninsyou As New Ninsyou()
    Private userId As String = Ninsyou.GetUserID()
    Private InputDate As String
    'CSV�A�b�v���[�h�������
    Private CsvInputMaxLineCount As String = CStr(System.Configuration.ConfigurationSettings.AppSettings("CsvInputMaxLineCount"))

    ''' <summary>���i�R�[�h���擾����</summary>
    ''' <returns>���i�R�[�h�f�[�^�e�[�u��</returns>
    ''' <history>2011/03/03�@�W���o�t(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetSyouhinCd() As Data.DataTable

        Return tokubetuTaiouMasterDA.SelSyouhinCd()

    End Function

    ''' <summary>�������@���擾����</summary>
    ''' <returns>�������@�f�[�^�e�[�u��</returns>
    ''' <history>2011/03/03�@�W���o�t(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetTyousaHouhou() As Data.DataTable

        Return tokubetuTaiouMasterDA.SelTyousaHouhou()

    End Function

    ''' <summary>�����X���i�������@���ʑΉ������擾����</summary>
    ''' <returns>�����X���i�������@���ʑΉ����f�[�^�e�[�v��</returns>
    ''' <remarks></remarks>
    Public Function GetTokubetuTaiouJyouhou(ByVal dtParamlist As Dictionary(Of String, String)) As Data.DataTable

        Return tokubetuTaiouMasterDA.SelTokubetuTaiouJyouhou(dtParamlist)

    End Function

    ''' <summary>������ʏ����擾</summary>
    ''' <returns>������ʏ��f�[�^�e�[�v��</returns>
    ''' <remarks></remarks>
    Public Function GetAitesakiSyubetuList() As Data.DataTable

        Return tokubetuTaiouMasterDA.SelAitesakiSyubetuList()

    End Function

    ''' <summary>�����X���i�������@���ʑΉ��S�������������擾����</summary>
    ''' <returns>�����X���i�������@���ʑΉ��S����������</returns>
    Public Function GetTokubetuTaiouCount(ByVal dtParamlist As Dictionary(Of String, String)) As Integer

        Return tokubetuTaiouMasterDA.SelTokubetuTaiouCount(dtParamlist)

    End Function

    ''' <summary>�����X���i�������@���ʑΉ��}�X�^�������ʂ�CSV�Ń_�E�����[�h</summary>
    ''' <returns>�����X���i�������@���ʑΉ��}�X�^�������ʂ�CSV�Ń_�E�����[�h�e�[�u��</returns>
    Public Function GetTokubetuTaiouJyouhouCSV(ByVal dtParamlist As Dictionary(Of String, String)) As Data.DataTable

        Return tokubetuTaiouMasterDA.SelTokubetuTaiouJyouhouCSV(dtParamlist)

    End Function


    ''' <summary>���ݒ���܂މ����X���i�������@���ʑΉ��}�X�^CSV�f�[�^���擾</summary>
    ''' <returns>���ݒ���܂މ����X���i�������@���ʑΉ��}�X�^CSV�e�[�u��</returns>
    Public Function GetTokubetuTaiouCSV(ByVal dtParamlist As Dictionary(Of String, String)) As Data.DataTable

        Return tokubetuTaiouMasterDA.SelTokubetuTaiouCSV(dtParamlist)

    End Function

    ''' <summary>���ݒ���܂މ����X���i�������@���ʑΉ��}�X�^CSV�f�[�^�������擾</summary>
    ''' <returns>���ݒ���܂މ����X���i�������@���ʑΉ��}�X�^CSV�f�[�^����</returns>
    Public Function GetTokubetuTaiouCSVCount(ByVal dtParamlist As Dictionary(Of String, String)) As Long

        Return tokubetuTaiouMasterDA.SelTokubetuTaiouCSVCount(dtParamlist)

    End Function

    ''' <summary>�A�b�v���[�h�Ǘ����擾����</summary>
    ''' <returns>�A�b�v���[�h�Ǘ��f�[�^�e�[�u��</returns>
    Public Function GetInputKanri() As Data.DataTable

        Return tokubetuTaiouMasterDA.SelInputKanri()

    End Function

    ''' <summary>�A�b�v���[�h�Ǘ��̌������擾����</summary>
    ''' <returns>�A�b�v���[�h�Ǘ��̌����f�[�^�e�[�u��</returns>
    Public Function GetInputKanriCount() As Integer

        Return tokubetuTaiouMasterDA.SelInputKanriCount()

    End Function

    ''' <summary>����於���擾����</summary>
    ''' <returns>����於�f�[�^�e�[�u��</returns>
    Public Function GetAitesakiMei(ByVal intAitesakiSyubetu As Integer, ByVal AitesakiCd As String, ByVal strTorikesiAitesaki As String) As Data.DataTable

        Return tokubetuTaiouMasterDA.SelAitesakiMei(intAitesakiSyubetu, AitesakiCd, strTorikesiAitesaki)

    End Function

    ''' <summary>CSV�t�@�C�����`�F�b�N����</summary>
    ''' <returns>CSV�t�@�C�����`�F�b�N����</returns>
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

        '�����X���i�������@���ʑΉ��}�X�^���쐬
        CreateOkDataTable(dtOk)

        '�����X���i�������@���ʑΉ��G���[�e�[�u�����쐬
        CreateErrorDataTable(dtError)

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
                    If i > CsvInputMaxLineCount Then
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

                    If strReadLine.Split(",")(0).Length < 10 Then
                        strReadLine = "0" & strReadLine
                    End If

                    '�J���}��ǉ�
                    If strReadLine.Split(",").Length < CsvInputCheck.TAIOU_FIELD_COUNT Then
                        strReadLine = strReadLine & StrDup(CsvInputCheck.TAIOU_FIELD_COUNT - strReadLine.Split(",").Length, ",")
                    End If

                    '�t�B�[���h���`�F�b�N
                    If Not commonCheck.ChkFieldCount(strReadLine.Split(",").Length, CsvInputCheck.TAIOU) Then
                        '�G���[�f�[�^�̏���
                        Call Me.ErrorLineSyori(i + 1, strReadLine, dtError)
                        Continue For
                    End If

                    '���ڍő咷�`�F�b�N
                    If Not commonCheck.ChkMaxLength(strReadLine, CsvInputCheck.TAIOU) Then
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
                    If Not commonCheck.ChkSuuti(strReadLine, CsvInputCheck.TAIOU) Then
                        '�G���[�f�[�^�̏���
                        Call Me.ErrorLineSyori(i + 1, strReadLine, dtError)
                        Continue For
                    End If

                    '�K�{�`�F�b�N
                    If Not commonCheck.ChkNotNull(strReadLine, CsvInputCheck.TAIOU) Then
                        '�G���[�f�[�^�̏���
                        Call Me.ErrorLineSyori(i + 1, strReadLine, dtError)
                        Continue For
                    End If

                    strReadLine = SetUmekusa(strReadLine)

                    '���݃`�F�b�N
                    If Not Me.ChkSonZai(strReadLine) Then
                        '�G���[�f�[�^�̏���
                        Call Me.ErrorLineSyori(i + 1, strReadLine, dtError)
                        Continue For
                    End If

                    '�X�V�����`�F�b�N
                    If Not commonCheck.ChkKousinJyouken(strReadLine, CsvInputCheck.TAIOU) Then

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
    ''' �����X�R�[�h��"0"�Ŗ��߂�
    ''' </summary>
    Public Function SetUmekusa(ByVal StrLine As String) As String

        Dim arrLine() As String = StrLine.Split(",")

        If "1".Equals(arrLine(1).ToString) AndAlso Not String.IsNullOrEmpty(arrLine(2).ToString) Then
            arrLine(2) = Right("00000" & arrLine(2).Trim, 5)
        End If

        If "5".Equals(arrLine(1).ToString) AndAlso Not String.IsNullOrEmpty(arrLine(2).ToString) Then
            arrLine(2) = Right("00000" & arrLine(2).Trim, 4)
        End If

        If "7".Equals(arrLine(1).ToString) AndAlso Not String.IsNullOrEmpty(arrLine(2).ToString) Then
            arrLine(2) = Right("00000" & arrLine(2).Trim, 4)
        End If
        'If arrLine(1).ToString <> String.Empty Then
        '    arrLine(1) = Right("00000" & arrLine(1).Trim, 5)
        'End If

        Return String.Join(",", arrLine)

    End Function
    ''' <summary>CSV�t�@�C�����捞����</summary>
    ''' <returns>CSV�t�@�C�����捞����</returns>
    Public Function CsvFileInput(ByVal dtOk As Data.DataTable, ByVal dtError As Data.DataTable, ByVal InputDate As String, ByVal userId As String, ByVal strFileMei As String, ByVal strEdiJouhouSakuseiDate As String) As Boolean

        Using scope As New TransactionScope(TransactionScopeOption.RequiresNew)
            Try
                '�����X�������@���ʑΉ��}�X�^��o�^
                If dtOk.Rows.Count > 0 Then
                    If Not tokubetuTaiouMasterDA.InsUpdTokubetuTaiou(dtOk) Then
                        Throw New ApplicationException
                    End If

                End If

                '�����X�������@���ʑΉ��G���[���e�[�u����o�^
                If dtError.Rows.Count > 0 Then
                    If Not tokubetuTaiouMasterDA.InsTokubetuTaiouError(dtError) Then
                        Throw New ApplicationException
                    End If
                End If

                '�A�b�v���[�h�Ǘ��e�[�u����o�^
                If Not tokubetuTaiouMasterDA.InsInputKanri(InputDate, strFileMei, strEdiJouhouSakuseiDate, CInt(IIf(dtError.Rows.Count, 1, 0)), userId) Then
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

        ''�����X�R�[�h
        'If Not tokubetuTaiouMasterDA.SelKameitenCd(intFieldCount.Split(",")(1).Trim) Then
        '    Return False
        'End If
        '�����(��ʁE�R�[�h)
        If Not tokubetuTaiouMasterDA.SelAitesakiSyubetuInput(CInt(intFieldCount.Split(",")(1)), intFieldCount.Split(",")(2).Trim) Then
            Return False
        End If

        '���i�R�[�h
        If Not tokubetuTaiouMasterDA.SelSyouhinCd(intFieldCount.Split(",")(4).Trim) Then
            Return False
        End If

        '�������@NO
        If Not tokubetuTaiouMasterDA.SelTyousahouhouNo(intFieldCount.Split(",")(6).Trim) Then
            Return False
        End If

        '���ʑΉ��R�[�h
        If Not tokubetuTaiouMasterDA.SelTokubetuCd(intFieldCount.Split(",")(8).Trim) Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>OK���C������</summary>
    Private Sub OkLineSyori(ByVal strLine As String, ByRef dtOk As Data.DataTable)

        'DB���݃`�F�b�N
        If tokubetuTaiouMasterDA.SelTokubetuTaiouJyouhou(strLine.Split(",")(1).Trim, strLine.Split(",")(2).Trim, strLine.Split(",")(4).Trim, strLine.Split(",")(6).Trim, strLine.Split(",")(8).Trim) Then
            Me.SetOkDataRow(strLine, dtOk, "1")
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
        'dr.Item("kameiten_cd") = strLine.Split(",")(1).Trim
        dr.Item("aitesaki_syubetu") = strLine.Split(",")(1).Trim
        dr.Item("aitesaki_cd") = strLine.Split(",")(2).Trim
        dr.Item("syouhin_cd") = strLine.Split(",")(4).Trim
        dr.Item("tys_houhou_no") = strLine.Split(",")(6).Trim
        dr.Item("tokubetu_taiou_cd") = strLine.Split(",")(8).Trim
        dr.Item("torikesi") = strLine.Split(",")(10).Trim
        dr.Item("kasan_syouhin_cd") = strLine.Split(",")(11).Trim
        dr.Item("syokiti") = strLine.Split(",")(12).Trim
        dr.Item("uri_kasan_gaku") = strLine.Split(",")(13).Trim
        dr.Item("koumuten_kasan_gaku") = strLine.Split(",")(14).Trim
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
        If strLine.Split(",").Length < CsvInputCheck.TAIOU_FIELD_COUNT Then
            intMaxCount = strLine.Split(",").Length
        Else
            intMaxCount = CsvInputCheck.TAIOU_FIELD_COUNT
        End If

        For i As Integer = 0 To intMaxCount - 1
            '�ő咷��؂���
            dr.Item(i) = commonCheck.CutMaxLength(strLine.Split(",")(i).Trim, commonCheck.TAIOU_MAX_LENGTH(i))
        Next

        '�s��
        dr.Item(CsvInputCheck.TAIOU_FIELD_COUNT + 1) = CStr(intLineNo)
        '��������
        dr.Item(CsvInputCheck.TAIOU_FIELD_COUNT + 2) = InputDate
        '�o�^���O�C�����[�UID
        dr.Item(CsvInputCheck.TAIOU_FIELD_COUNT + 3) = userId

        dtError.Rows.Add(dr)

    End Sub

    ''' <summary>�����X���i�������@���ʑΉ��}�X�^���쐬</summary>
    Public Sub CreateOkDataTable(ByRef dtOk As Data.DataTable)
        dtOk.Columns.Add("key")                     '--�L�[(������ʁA�����R�[�h�A���i�R�[�h�A�������@NO�A���ʑΉ��R�[�h)
        dtOk.Columns.Add("ins_upd_flg")             '--�ǉ��X�VFLG(0:�ǉ�; 1:�X�V)
        'dtOk.Columns.Add("kameiten_cd")            '--�����X�R�[�h
        dtOk.Columns.Add("aitesaki_syubetu")        '--�������
        dtOk.Columns.Add("aitesaki_cd")             '--�����R�[�h
        dtOk.Columns.Add("syouhin_cd")              '--���i�R�[�h
        dtOk.Columns.Add("tys_houhou_no")           '--�������@NO
        dtOk.Columns.Add("tokubetu_taiou_cd")       '--���ʑΉ��R�[�h
        dtOk.Columns.Add("torikesi")                '--���
        dtOk.Columns.Add("kasan_syouhin_cd")        '--���z���Z���i�R�[�h
        dtOk.Columns.Add("syokiti")                 '--�����l
        dtOk.Columns.Add("uri_kasan_gaku")          '--���������Z���z
        dtOk.Columns.Add("koumuten_kasan_gaku")     '--�H���X�������Z���z
        dtOk.Columns.Add("add_login_user_id")       '--�o�^��ID
        dtOk.Columns.Add("add_datetime")            '--�o�^����
        dtOk.Columns.Add("upd_login_user_id")       '--�X�V��ID
        dtOk.Columns.Add("upd_datetime")            '--�X�V����
    End Sub

    ''' <summary>�����X���i�������@���ʑΉ��G���[�}�X�^���쐬</summary>
    Public Sub CreateErrorDataTable(ByRef dtError As Data.DataTable)
        dtError.Columns.Add("edi_jouhou_sakusei_date")      '--EDI���쐬��
        dtError.Columns.Add("aitesaki_syubetu")             '--�������
        dtError.Columns.Add("aitesaki_cd")                  '--�����R�[�h
        dtError.Columns.Add("aitesaki_mei")                 '--�����R�[�h
        'dtError.Columns.Add("kameiten_cd")                 '--�����X�R�[�h
        'dtError.Columns.Add("kameiten_mei")                '--�����X��
        dtError.Columns.Add("syouhin_cd")                   '--���i�R�[�h
        dtError.Columns.Add("syouhin_mei")                  '--���i��
        dtError.Columns.Add("tys_houhou_no")                '--�������@NO
        dtError.Columns.Add("tys_houhou")                   '--�������@
        dtError.Columns.Add("tokubetu_taiou_cd")            '--���ʑΉ��R�[�h
        dtError.Columns.Add("tokubetu_taiou_meisyou")       '--���ʑΉ�����
        dtError.Columns.Add("torikesi")                     '--���
        dtError.Columns.Add("kasan_syouhin_cd")             '--���z���Z���i�R�[�h
        dtError.Columns.Add("kasan_syouhin_mei")            '--���z���Z���i��
        dtError.Columns.Add("syokiti")                      '--�����l
        dtError.Columns.Add("uri_kasan_gaku")               '--���������Z���z
        dtError.Columns.Add("koumuten_kasan_gaku")          '--�H���X�������Z���z
        dtError.Columns.Add("gyou_no")                      '--�sNO
        dtError.Columns.Add("syori_datetime")               '--��������
        dtError.Columns.Add("add_login_user_id")            '--�o�^��ID
        dtError.Columns.Add("add_datetime")                 '--�o�^����
        dtError.Columns.Add("upd_login_user_id")            '--�X�V��ID
        dtError.Columns.Add("upd_datetime")                 '--�X�V����
    End Sub

    ''' <summary>OK�f�[�^��key��ݒ�</summary>
    Public Function GetDataKey(ByVal strLine As String) As String

        Return strLine.Split(",")(1).Trim & "," & strLine.Split(",")(2).Trim & "," & strLine.Split(",")(4).Trim & "," & strLine.Split(",")(6).Trim & "," & strLine.Split(",")(8).Trim & strLine.Split(",")(10).Trim

    End Function

    ''' <summary>�����X���i�������@���ʑΉ��}�X�^�G���[�f�[�^���擾</summary>
    Public Function GetTokubetuTaiouError(ByVal strEdidate As String, ByVal strSyoridate As String) As Data.DataTable
        Return tokubetuTaiouMasterDA.SelTokubetuTaiouError(strEdidate, strSyoridate)
    End Function

    ''' <summary>�����X���i�������@���ʑΉ��}�X�^�G���[�������擾</summary>
    Public Function GetTokubetuTaiouErrorCount(ByVal strEdidate As String, ByVal strSyoridate As String) As Integer
        Return tokubetuTaiouMasterDA.SelTokubetuTaiouErrorCount(strEdidate, strSyoridate)
    End Function

    ''' <summary>�����X���i�������@���ʑΉ��}�X�^�G���[CSV�f�[�^���擾</summary>
    Public Function GetTokubetuTaiouErrorCSV(ByVal strEdidate As String, ByVal strSyoridate As String) As Data.DataTable
        Return tokubetuTaiouMasterDA.SelTokubetuTaiouErrorCSV(strEdidate, strSyoridate)
    End Function


    ''' <summary>�����X���i�������@���ʑΉ��}�X�^�����擾(�u�n��E�c�Ə��E�w�薳�����Ώۃ`�F�b�N�{�b�N�X�v=�`�F�b�N�̏ꍇ)</summary>
    ''' <history>2012/05/23 �ԗ� 407553�̑Ή� �ǉ�</history>
    Public Function GetTokubetuTaiouNasiInfo(ByVal dtParamList As Dictionary(Of String, String)) As Data.DataTable

        '�߂�l
        Return tokubetuTaiouMasterDA.SelTokubetuTaiouNasiInfo(dtParamList)
    End Function

    ''' <summary>�����X���i�������@���ʑΉ��}�X�^�������擾�u�n��E�c�Ə��E�w�薳�����Ώۃ`�F�b�N�{�b�N�X�v=�`�F�b�N�̏ꍇ</summary>
    ''' <history>2012/05/23 �ԗ� 407553�̑Ή� �ǉ�</history>
    Public Function GetTokubetuTaiouNasiCount(ByVal dtParamList As Dictionary(Of String, String)) As Integer

        '�߂�l
        Return tokubetuTaiouMasterDA.SelTokubetuTaiouNasiCount(dtParamList)
    End Function

    ''' <summary>
    ''' ���l�����̎擾
    ''' </summary>
    ''' <history>2013/05/20 �k�o 407584�̑Ή� �ǉ�</history>
    Public Function GetStyleMeisyou() As Object

        '�߂�l
        Return tokubetuTaiouMasterDA.SelStyleMeisyou()

    End Function

End Class
