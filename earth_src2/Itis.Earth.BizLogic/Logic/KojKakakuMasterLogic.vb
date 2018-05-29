Imports System.Transactions
Imports Itis.Earth.DataAccess
Public Class KojKakakuMasterLogic
    ''' <summary>�H�����i�}�X�^�N���X�̃C���X�^���X���� </summary>
    Private KojKakakuMasterDA As New KojKakakuMasterDataAccess
    Public Function GetKojKakakuInfoCount(ByVal dtInfo As DataTable) As Integer
        Return KojKakakuMasterDA.SelKojKakakuInfoCount(dtInfo)
    End Function
    Public Function GetKojKakakuInfo(ByVal dtInfo As DataTable) As Data.DataTable
        Return KojKakakuMasterDA.SelKojKakakuSeiteInfo(dtInfo)
    End Function
    Public Function GetKojKakakuCSVInfo(ByVal dtInfo As DataTable) As Data.DataTable
        Return KojKakakuMasterDA.SelKojKakakuCSVInfo(dtInfo)
    End Function
    ''' <summary>���i���擾����</summary>
    ''' <returns>���i�f�[�^�e�[�u��</returns>
    Public Function GetSyouhin(Optional ByVal syohuinCd As String = "") As Data.DataTable
        Return KojKakakuMasterDA.SelSyouhin(syohuinCd)
    End Function
    ''' <summary>�A�b�v���[�h�Ǘ����擾����</summary>
    ''' <returns>�A�b�v���[�h�Ǘ��f�[�^�e�[�u��</returns>
    Public Function GetUploadKanri() As Data.DataTable
        Return KojKakakuMasterDA.SelUploadKanri()
    End Function
    ''' <summary>�A�b�v���[�h�Ǘ��̌������擾����</summary>
    ''' <returns>�A�b�v���[�h�Ǘ��̌���</returns>
    Public Function GetUploadKanriCount() As Integer
        Return KojKakakuMasterDA.SelUploadKanriCount()
    End Function

    ''' <summary>CSV�t�@�C�����`�F�b�N����</summary>
    ''' <param name="fupId">�A�b�v���[�h</param>
    ''' <param name="strUmuFlg">�G���[�L���t���O</param>
    ''' <returns>CSV�t�@�C�����`�F�b�N����</returns>
    Public Function ChkCsvFile(ByVal fupId As Web.UI.WebControls.FileUpload, ByRef strUmuFlg As String) As String

        Dim myStream As IO.Stream                               '���o�̓X�g���[��
        Dim myReader As IO.StreamReader                         '�X�g���[�����[�_�[
        Dim strReadLine As String                               '�捞�t�@�C���Ǎ��ݍs
        Dim intLineCount As Integer = 0                         '���C����
        Dim strCsvLine() As String                              'CSV�t�@�C�����e
        Dim strNyuuryokuFileMei As String                       'CSV�t�@�C����
        Dim strEdiJouhouSakuseiDate As String = String.Empty    'EDI���쐬��
        Dim dtKojKakakuOk As New Data.DataTable               '�H�����i�}�X�^
        Dim dtKojKakakuError As New Data.DataTable           '�H�����i�G���[
        Dim strUploadDate As String                             '�A�b�v���[�h����
        Dim ninsyou As New Ninsyou()
        Dim strUserId As String                                 '���[�U�[ID
        Dim strMaxUpload As String                              'CSV�捞�̏������
        Dim commonCheck As New CsvInputCheck
        '�A�b�v���[�h����
        strUploadDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")
        '���[�U�[ID
        strUserId = ninsyou.GetUserID()
        'CSV�t�@�C����
        strNyuuryokuFileMei = commonCheck.CutMaxLength(fupId.FileName, 128)
        'CSV�捞�̏������
        strMaxUpload = System.Configuration.ConfigurationManager.AppSettings("CsvInputMaxLineCount").ToString
        '���o�̓X�g���[��
        myStream = fupId.FileContent
        '�X�g���[�����[�_�[
        myReader = New IO.StreamReader(myStream, System.Text.Encoding.GetEncoding(932))
        '�H�����i�}�X�^���쐬����
        Call SetKojKakakuOk(dtKojKakakuOk)
        '�H�����i�G���[�e�[�u�����쐬����
        Call SetKojKakakuError(dtKojKakakuError)

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
                    If i > CInt(strMaxUpload) Then
                        Return String.Format(Messages.Instance.MSG040E, strMaxUpload)
                    ElseIf i < 1 Then
                        Return String.Format(Messages.Instance.MSG048E)
                    Else
                        Exit For
                    End If
                End If
            Next

            'EDI���쐬��
            strEdiJouhouSakuseiDate = Right("          " & strCsvLine(1).Split(",")(0), 10)

            'CSV�t�@�C�����`�F�b�N
            For i As Integer = 1 To strCsvLine.Length - 1
                strReadLine = strCsvLine(i)
                If (Not strReadLine Is Nothing) AndAlso (Replace(strReadLine, ",", "") <> "") Then
                    'EDI���쐬�����O0���ߕϊ�
                    If strReadLine.Split(",")(0).Length < 10 Then
                        Dim arrLine() As String = Split(strReadLine, ",")
                        arrLine(0) = Right("          " & arrLine(0).Trim, 10)
                        strReadLine = String.Join(",", arrLine)
                    End If
                    '�������ߕϊ�
                    If strReadLine.Split(",").Length < CsvInputCheck.KOJ_FIELD_COUNT Then
                        strReadLine = strReadLine & StrDup(CsvInputCheck.KOJ_FIELD_COUNT - strReadLine.Split(",").Length, ",")
                    End If
                    '�t�B�[���h���`�F�b�N
                    If Not commonCheck.ChkFieldCount(strReadLine.Split(",").Length, CsvInputCheck.KOJ) Then
                        '�G���[�f�[�^���쐬����
                        Call SetKojKakakuErrData(i + 1, strReadLine, dtKojKakakuError)
                        Continue For
                    End If
                    '���ڍő咷�`�F�b�N
                    If Not commonCheck.ChkMaxLength(strReadLine, CsvInputCheck.KOJ) Then
                        '�G���[�f�[�^���쐬����
                        Call SetKojKakakuErrData(i + 1, strReadLine, dtKojKakakuError)
                        Continue For
                    End If
                    '�֑������`�F�b�N
                    If Not commonCheck.ChkKinjiMoji(strReadLine) Then
                        '�G���[�f�[�^���쐬����
                        Call SetKojKakakuErrData(i + 1, strReadLine, dtKojKakakuError)
                        Continue For
                    End If
                    '���l�^���ڃ`�F�b�N
                    If Not commonCheck.ChkSuuti(strReadLine, CsvInputCheck.KOJ) Then
                        '�G���[�f�[�^���쐬����
                        Call SetKojKakakuErrData(i + 1, strReadLine, dtKojKakakuError)
                        Continue For
                    End If
                    '�K�{�`�F�b�N
                    If Not commonCheck.ChkNotNull(strReadLine, CsvInputCheck.KOJ) Then
                        '�G���[�f�[�^���쐬����
                        Call SetKojKakakuErrData(i + 1, strReadLine, dtKojKakakuError)
                        Continue For
                    End If
                    '�����R�[�h���O0���ߕϊ�
                    SetAitesakiCd(strReadLine)

                    '���݃`�F�b�N
                    If Not ChkMstSonZai(strReadLine) Then
                        '�G���[�f�[�^���쐬����
                        Call SetKojKakakuErrData(i + 1, strReadLine, dtKojKakakuError)
                        Continue For
                    End If
                    '�X�V�����`�F�b�N
                    If Not commonCheck.ChkKousinJyouken(strReadLine, CsvInputCheck.KOJ) Then
                        Continue For
                    End If
                    '�X�V��ǉ��𔻒f����
                    Call SetKousinKbn(strReadLine, dtKojKakakuOk)
                End If
            Next
            '�G���[�L���t���O��ݒ肷��
            If dtKojKakakuError.Rows.Count > 0 Then
                strUmuFlg = "1"
            End If
            'CSV�t�@�C�����捞
            If Not CsvFileUpload(dtKojKakakuOk, dtKojKakakuError, strUploadDate, strUserId, strNyuuryokuFileMei, strEdiJouhouSakuseiDate) Then
                Return Messages.Instance.MSG050E
            End If
        Catch ex As Exception
            Return Messages.Instance.MSG049E
        End Try

        Return String.Empty
    End Function
    ''' <summary>�H�����i�e�[�u�����쐬����</summary>
    ''' <param name="dtKojKakakuOk">�H�����i�e�[�u��</param>
    Public Sub SetKojKakakuOk(ByRef dtKojKakakuOk As Data.DataTable)

        Dim dc As Data.DataColumn

        dc = New Data.DataColumn
        dc.ColumnName = "key"               '�L�[(������� + �����R�[�h + ���i�R�[�h +�H����ЃR�[�h )
        dtKojKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "ins_upd_flg"       '�ǉ��X�VFLG(0:�ǉ�; 1:�X�V)
        dtKojKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "aitesaki_syubetu"  '�������
        dtKojKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "aitesaki_cd"       '�����R�[�h
        dtKojKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "syouhin_cd"        '���i�R�[�h
        dtKojKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "koj_gaisya_cd"     '�H����ЃR�[�h
        dtKojKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "koj_gaisya_jigyousyo_cd"     '�H����Ў��Ə��R�[�h
        dtKojKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "torikesi"          '���
        dtKojKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "uri_gaku"  '������z
        dtKojKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "koj_gaisya_seikyuu_umu"   '�H����А����L��
        dtKojKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "seikyuu_umu"  '�����L��
        dtKojKakakuOk.Columns.Add(dc)
       
    End Sub
    ''' <summary>�H�����i�G���[�e�[�u�����쐬����</summary>
    ''' <param name="dtKojKakakuError">�H�����i�G���[�e�[�u��</param>
    Public Sub SetKojKakakuError(ByRef dtKojKakakuError As Data.DataTable)

        Dim dc As Data.DataColumn
        dc = New Data.DataColumn
        dc.ColumnName = "edi_jouhou_sakusei_date"           'EDI���쐬��
        dtKojKakakuError.Columns.Add(dc)

        dc = New Data.DataColumn
        dc.ColumnName = "aitesaki_syubetu"                  '�������
        dtKojKakakuError.Columns.Add(dc)

        dc = New Data.DataColumn
        dc.ColumnName = "aitesaki_cd"                       '�����R�[�h
        dtKojKakakuError.Columns.Add(dc)

        dc = New Data.DataColumn
        dc.ColumnName = "aitesaki_mei"                      '����於
        dtKojKakakuError.Columns.Add(dc)

        dc = New Data.DataColumn
        dc.ColumnName = "syouhin_cd"                        '���i�R�[�h
        dtKojKakakuError.Columns.Add(dc)

        dc = New Data.DataColumn
        dc.ColumnName = "syouhin_mei"                       '���i��
        dtKojKakakuError.Columns.Add(dc)

        dc = New Data.DataColumn
        dc.ColumnName = "koj_cd"                     '�H����ЃR�[�h
        dtKojKakakuError.Columns.Add(dc)

        dc = New Data.DataColumn
        dc.ColumnName = "koj_gaisya_mei"                    '�H����Ж�
        dtKojKakakuError.Columns.Add(dc)

        dc = New Data.DataColumn
        dc.ColumnName = "torikesi"                          '���
        dtKojKakakuError.Columns.Add(dc)

        dc = New Data.DataColumn
        dc.ColumnName = "uri_gaku"                          '������z
        dtKojKakakuError.Columns.Add(dc)

        dc = New Data.DataColumn
        dc.ColumnName = "koj_gaisya_seikyuu_umu"            '�H����А����L��
        dtKojKakakuError.Columns.Add(dc)

        dc = New Data.DataColumn
        dc.ColumnName = "seikyuu_umu"                       '�����L��
        dtKojKakakuError.Columns.Add(dc)

        dc = New Data.DataColumn
        dc.ColumnName = "gyou_no"                           '�sNO
        dtKojKakakuError.Columns.Add(dc)
    End Sub
    ''' <summary>�H�����i�G���[�f�[�^���쐬����</summary>
    ''' <param name="intLineNo">CSV�t�@�C���̊Y���s�̍sNO</param>
    ''' <param name="strLine">CSV�t�@�C���̊Y���s�̃f�[�^</param>
    ''' <param name="dtKojKakakuError">�H�����i�G���[�f�[�^</param>
    Private Sub SetKojKakakuErrData(ByVal intLineNo As Integer, _
                                       ByVal strLine As String, _
                                       ByRef dtKojKakakuError As Data.DataTable)

        Dim intMaxCount As Integer
        Dim commonCheck As New CsvInputCheck
        Dim dr As Data.DataRow
        dr = dtKojKakakuError.NewRow

        If strLine.Split(",").Length < CsvInputCheck.KOJ_FIELD_COUNT Then
            intMaxCount = strLine.Split(",").Length
        Else
            intMaxCount = CsvInputCheck.KOJ_FIELD_COUNT
        End If

        For i As Integer = 0 To intMaxCount - 1
            '�ő咷��؂���
            dr.Item(i) = commonCheck.CutMaxLength(strLine.Split(",")(i).Trim, commonCheck.KOJ_MAX_LENGTH(i))
        Next
        '�s��
        dr.Item(CsvInputCheck.KOJ_FIELD_COUNT) = CStr(intLineNo)

        dtKojKakakuError.Rows.Add(dr)

    End Sub
    ''' <summary>�O0���ߕϊ�</summary>
    ''' <param name="strLine">CSV�t�@�C���̊Y���s�̃f�[�^</param>
    Private Sub SetAitesakiCd(ByRef strLine As String)

        Dim arrLine() As String = Split(strLine, ",")

        Select Case arrLine(1)
            Case "1"
                arrLine(2) = Right("00000" & arrLine(2).Trim, 5)
            Case "5"
                If arrLine(2).Length < 4 Then
                    arrLine(2) = Right("0000" & arrLine(2).Trim, 4)
                End If
            Case "7"
                If arrLine(2).Length < 4 Then
                    arrLine(2) = Right("0000" & arrLine(2).Trim, 4)
                End If
        End Select
        If arrLine(6).Trim.ToUpper <> "ALLAL" Then
            arrLine(6) = Right("000000" & arrLine(6).Trim, 6)
        End If
        strLine = String.Join(",", arrLine)

    End Sub
    ''' <summary>���݃`�F�b�N</summary>
    ''' <param name="strLine">CSV�t�@�C���̊Y���s�̃f�[�^</param>
    Private Function ChkMstSonZai(ByVal strLine As String) As Boolean

        Dim HanbaiKakakuMasterDA As New HanbaiKakakuMasterDataAccess


        '�����(��ʁE�R�[�h)
        If Not HanbaiKakakuMasterDA.SelAiteSaki(strLine.Split(",")(1).Trim, strLine.Split(",")(2).Trim) Then
            Return False
        End If

        '���i�R�[�h
        If GetSyouhin(strLine.Split(",")(4).Trim).Rows.Count = 0 Then
            Return False
        End If

        '�H�����NO
        If strLine.Split(",")(6).Trim <> "ALLAL" Then
            If GetKojKaisyaKensaku(strLine.Split(",")(6).Trim).Rows.Count <= 0 Then
                Return False
            End If
        End If


        Return True
    End Function
    Public Function GetKojKaisyaKensaku(ByVal strCd As String) As DataTable

        Return KojKakakuMasterDA.SelKojKaisyaKensaku(strCd)

    End Function

    ''' <summary>�X�V��ǉ��𔻒f����</summary>
    ''' <param name="strLine">CSV�t�@�C���̊Y���s�̃f�[�^</param>
    ''' <param name="dtKojKakakuOk">�H�����i�f�[�^</param> 
    Private Sub SetKousinKbn(ByVal strLine As String, ByRef dtKojKakakuOk As Data.DataTable)

        '�H�����i�}�X�^���݃`�F�b�N
        If KojKakakuMasterDA.SelKojKakaku(strLine.Split(",")(1).Trim, strLine.Split(",")(2).Trim, strLine.Split(",")(4).Trim, strLine.Split(",")(6).Trim) Then
            '�H�����i�}�X�^�����݂���ꍇ�A�X�V�敪��ݒ肷��
            Call SetKojKakakuDataRow(strLine, dtKojKakakuOk, "1")
        Else
            'CSV�t�@�C���ɍs�O�̃f�[�^������ꍇ
            If dtKojKakakuOk.Rows.Count > 0 Then
                '���݃t���O
                Dim blnSonzaiFlg As Boolean = False

                '�s�O�f�[�^�Ɏ�L�[�����邩�ǂ����𔻒f����
                For i As Integer = 0 To dtKojKakakuOk.Rows.Count - 1
                    If dtKojKakakuOk.Rows(i).Item("key").ToString.Trim.Equals(GetKojKakakuKey(strLine)) Then
                        blnSonzaiFlg = True
                        Exit For
                    End If
                Next

                If blnSonzaiFlg Then
                    '�s�O�f�[�^�Ɏ�L�[������ꍇ�A�X�V�敪��ݒ肷��
                    Call SetKojKakakuDataRow(strLine, dtKojKakakuOk, "1")
                Else
                    '�s�O�f�[�^�Ɏ�L�[���Ȃ��ꍇ�A�ǉ��敪��ݒ肷��
                    Call SetKojKakakuDataRow(strLine, dtKojKakakuOk, "0")
                End If
            Else
                '�H�����i�}�X�^�����݂��Ȃ��ACSV�t�@�C���ɍs�O�̃f�[�^���Ȃ��ꍇ�A�ǉ��敪��ݒ肷��
                Call SetKojKakakuDataRow(strLine, dtKojKakakuOk, "0")
            End If

        End If

    End Sub
    ''' <summary>�H�����i�f�[�^���쐬����</summary>
    ''' <param name="strLine">CSV�t�@�C���̊Y���s�̃f�[�^</param>
    ''' <param name="dtKojKakakuOk">�H�����i�f�[�^</param>
    ''' <param name="strInsUpdFlg">�X�V�E�ǉ��敪</param>
    Public Sub SetKojKakakuDataRow(ByVal strLine As String, _
                                      ByRef dtKojKakakuOk As Data.DataTable, _
                                      ByVal strInsUpdFlg As String)

        Dim dr As Data.DataRow
        dr = dtKojKakakuOk.NewRow
        dr.Item("key") = GetKojKakakuKey(strLine)                '��L�[
        dr.Item("ins_upd_flg") = strInsUpdFlg                       '�X�V�E�ǉ��敪
        dr.Item("aitesaki_syubetu") = strLine.Split(",")(1).Trim    '�������
        dr.Item("aitesaki_cd") = strLine.Split(",")(2).Trim         '�����R�[�h
        dr.Item("syouhin_cd") = strLine.Split(",")(4).Trim          '���i�R�[�h
        dr.Item("koj_gaisya_cd") = Left(Right("       " & strLine.Split(",")(6).Trim, 7), 5).Trim        '�H����ЃR�[�h
        dr.Item("koj_gaisya_jigyousyo_cd") = Right(strLine.Split(",")(6).Trim, 2)     '�H����Ў��Ə��R�[�h
        dr.Item("torikesi") = IIf(strLine.Split(",")(8).Trim.Equals(String.Empty), "0", strLine.Split(",")(8).Trim) '���
        dr.Item("uri_gaku") = strLine.Split(",")(9).Trim               '������z
        dr.Item("koj_gaisya_seikyuu_umu") = strLine.Split(",")(10).Trim   '�H����А����L��
        dr.Item("seikyuu_umu") = strLine.Split(",")(11).Trim                  '�����L��


        dtKojKakakuOk.Rows.Add(dr)

    End Sub
    ''' <summary>�H�����i�f�[�^��key��ݒ肷��</summary>
    ''' <param name="strLine">CSV�t�@�C���̊Y���s�̃f�[�^</param>
    ''' <returns>�Y���s�̃f�[�^�̎�L�[</returns>
    Public Function GetKojKakakuKey(ByVal strLine As String) As String

        Return strLine.Split(",")(1).Trim & "," & strLine.Split(",")(2).Trim & "," & strLine.Split(",")(4).Trim & "," & strLine.Split(",")(6).Trim

    End Function

    ''' <summary>CSV�t�@�C�����捞����</summary>
    ''' <param name="dtKojKakakuOk">�H�����i�f�[�^</param>
    ''' <param name="dtKojKakakuError">�H�����i�G���[�f�[�^</param>
    ''' <param name="strUserId">���[�U�[ID</param>
    ''' <param name="strNyuuryokuFileMei">���̓t�@�C����</param>
    ''' <param name="strEdiJouhouSakuseiDate">EDI���쐬��</param>
    ''' <returns>�����E���s�敪</returns>
    Public Function CsvFileUpload(ByVal dtKojKakakuOk As Data.DataTable, _
                                  ByVal dtKojKakakuError As Data.DataTable, _
                                  ByVal strUploadDate As String, _
                                  ByVal strUserId As String, _
                                  ByVal strNyuuryokuFileMei As String, _
                                  ByVal strEdiJouhouSakuseiDate As String) As Boolean

        Using scope As New TransactionScope(TransactionScopeOption.RequiresNew)
            Try
                '�H�����i�}�X�^��o�^����
                If dtKojKakakuOk.Rows.Count > 0 Then
                    If Not KojKakakuMasterDA.InsUpdKojKakaku(dtKojKakakuOk, strUserId) Then
                        Throw New ApplicationException
                    End If
                End If
                '�H�����i�G���[���e�[�u����o�^����
                If dtKojKakakuError.Rows.Count > 0 Then
                    If Not KojKakakuMasterDA.InsKojKakakuError(dtKojKakakuError, strUploadDate, strUserId) Then
                        Throw New ApplicationException
                    End If
                End If
                '�A�b�v���[�h�Ǘ��e�[�u����o�^����
                If Not KojKakakuMasterDA.InsUploadKanri(strUploadDate, strNyuuryokuFileMei, strEdiJouhouSakuseiDate, CInt(IIf(dtKojKakakuError.Rows.Count, 1, 0)), strUserId) Then
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
    ''' <summary>�H�����i�G���[�����擾����</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI���쐬��</param>
    ''' <param name="strSyoriDate">��������</param>
    ''' <returns>�H�����i�G���[�f�[�^�e�[�u��</returns>
    Public Function GetKojKakakuErr(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoriDate As String) _
                As DataTable
        Return KojKakakuMasterDA.SelKojKakakuErr(strEdiJouhouSakuseiDate, strSyoriDate)
    End Function
    ''' <summary>�H�����i�G���[�������擾����</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI���쐬��</param>
    ''' <param name="strSyoriDate">��������</param>
    ''' <returns>�H�����i�G���[����</returns>
    Public Function GetKojKakakuErrCount(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoriDate As String) As Integer
        Return KojKakakuMasterDA.SelKojKakakuErrCount(strEdiJouhouSakuseiDate, strSyoriDate)
    End Function

    ''' <summary>�H�����i�G���[CSV�����擾����</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI���쐬��</param>
    ''' <param name="strSyoriDate">��������</param>
    ''' <returns>�H�����i�G���[CSV�f�[�^�e�[�u��</returns>
    Public Function SelKojKakakuErrCsv(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoriDate As String) _
                    As DataTable
        Return KojKakakuMasterDA.SelKojKakakuErrCsv(strEdiJouhouSakuseiDate, strSyoriDate)
    End Function

    ''' <summary>�H�����i�}�X�^�ʐݒ�f�[�^���擾����</summary>
    Public Function GetHanbaiKakakuKobeituSettei(ByVal strAiteSakiSyubetu As String, ByVal strAiteSakiCd As String, ByVal strSyouhinCd As String, ByVal strKojKaisyaCd As String) As Data.DataTable

        Dim dtParam As New DataTable
        Dim row As DataRow = dtParam.NewRow
        dtParam.Columns.Add(New DataColumn("aitesaki_syubetu", GetType(String)))
        dtParam.Columns.Add(New DataColumn("aitesaki_cd_from", GetType(String)))
        dtParam.Columns.Add(New DataColumn("syouhin_cd", GetType(String)))
        dtParam.Columns.Add(New DataColumn("kojkaisya_cd", GetType(String)))
        dtParam.Columns.Add(New DataColumn("aitesaki_cd_to", GetType(String)))
        dtParam.Columns.Add(New DataColumn("torikesi", GetType(String)))
        dtParam.Columns.Add(New DataColumn("torikesi_aitesaki", GetType(String)))

        '������ʂ�ݒ肷��
        row.Item("aitesaki_syubetu") = strAiteSakiSyubetu
        '�����R�[�hFROM��ݒ肷��
        row.Item("aitesaki_cd_from") = strAiteSakiCd
        '���i�R�[�h��ݒ肷��
        row.Item("syouhin_cd") = strSyouhinCd
        '�H�����
        row.Item("kojkaisya_cd") = strKojKaisyaCd

        row.Item("aitesaki_cd_to") = ""
        row.Item("torikesi") = ""
        row.Item("torikesi_aitesaki") = ""
        dtParam.Rows.Add(row)
        Return KojKakakuMasterDA.SelKojKakakuKobeituSettei(dtParam)

    End Function
    ''' <summary>�H�����i�}�X�^�ʐݒ�̑��݃`�F�c�N</summary>
    Public Function CheckSonzai(ByVal strAiteSakiSyubetu As String, ByVal strAiteSakiCd As String, ByVal strSyouhinCd As String, ByVal strKojKaisyaCd As String) As Data.DataTable

        Return KojKakakuMasterDA.CheckSonzai(strAiteSakiSyubetu, strAiteSakiCd, strSyouhinCd, strKojKaisyaCd)

    End Function

    ''' <summary>�H�����i�}�X�^�ʐݒ�̓o�^</summary>
    Public Function SetKojKakakuKobeituSettei(ByVal dtKojKakakuOk As Data.DataTable, ByVal strUserId As String) As Boolean

        Return KojKakakuMasterDA.InsUpdKojKakaku(dtKojKakakuOk, strUserId)

    End Function

End Class
