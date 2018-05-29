Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports System.Transactions
Imports System.Data
Imports System.Data.SqlClient
Imports Lixil.JHS_EKKS.Utilities
Imports Lixil.JHS_EKKS.DataAccess

''' <summary>
''' �x�X ���ʌv��l �b�r�u�捞BC
''' </summary>
''' <remarks></remarks>
''' <history>
''' <para>2012/11/26 P-44979 ���� �V�K�쐬 </para>
''' </history>
Public Class SitenTukibetuKeikakuchiInputBC

    '�x�X ���ʌv��l �b�r�u�捞DA
    Private sitenTukibetuKeikakuchiInputDA As New SitenTukibetuKeikakuchiInputDA
    'CommonDA
    Private commonDA As New CommonDA

    '�e�[�u���u�x�X�ʌ��ʌv��Ǘ��e�[�u���v�̍��ڍő咷
    Private SITENBETU_MAX_LENGTH() As Integer = {2, 40, 4, 4, 40, 1, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12}
    '�e�[�u���u�x�X�ʌ��ʌv��Ǘ��\_�捞�G���[���e�[�u���v�̍��ڍő咷
    Private SITENBETUERROR_MAX_LENGTH() As Integer = {40, 12, 20, 80, 30, 20, 30, 20}
    '�g�p�֎~������z��
    Private arrayKinsiStr() As String = New String() {vbTab, """", "�C", "'", "<", ">", "&", "$$$"}
    '�e�[�u���u�x�X�ʌ��ʌv��Ǘ��e�[�u���v�̐��l�^���ڍ���
    Private HANBAI_NUM_INDEX() As Integer = {1, 2, 3, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41}
    '�e�[�u���u�x�X�ʌ��ʌv��Ǘ��e�[�u���v�̕K�{���͍��ڍ���
    Private HANBAI_NOTNULL_INDEX() As Integer = {0, 1, 2, 3, 5}

    ''' <summary>
    ''' ��ʈꗗ�f�[�^�擾����
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <returns>�A�b�v���[�h�Ǘ��e�[�u���̃f�[�^</returns>
    ''' <remarks>�A�b�v���[�h�Ǘ��e�[�u���̃f�[�^���擾����</remarks>
    ''' <history>
    ''' <para>2012/11/23 P-44979 ���� �V�K�쐬 </para>
    ''' </history>
    Public Function SelTUploadKanri(ByVal strKbn As String) As Data.DataTable
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKbn)

        Return sitenTukibetuKeikakuchiInputDA.SelTUploadKanri(strKbn)

    End Function

    ''' <summary>
    ''' CSV�捞����
    ''' </summary>
    ''' <param name="fupId">�捞�t�@�C��</param>
    ''' <param name="strUmuFlg">��</param>
    ''' <returns>�G���[���b�Z�[�W�ԍ�</returns>
    ''' <remarks>CSV�捞����</remarks>
    ''' <history>
    ''' <para>2012/11/23 P-44979 ���� �V�K�쐬 </para>
    ''' </history>
    Public Function ChkCsvFile(ByVal fupId As Web.UI.WebControls.FileUpload, ByRef strUmuFlg As String) As String

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, fupId, strUmuFlg)

        Dim myStream As IO.Stream                                '���o�̓X�g���[��
        Dim myReader As IO.StreamReader                          '�X�g���[�����[�_�[
        Dim strReadLine As String                                '�捞�t�@�C���Ǎ��ݍs
        Dim intLineCount As Integer = 0                          '���C����
        Dim strCsvLine() As String                               'CSV�t�@�C�����e
        Dim strNyuuryokuFileMei As String                        'CSV�t�@�C����
        Dim dtTSitenbetuTukiKeikakuKanriOk As New Data.DataTable '�x�X�ʌ��ʌv��Ǘ��e�[�u��
        Dim dtTSitenTukibetuTorikomiError As New Data.DataTable  '�x�X�ʌ��ʌv��Ǘ��\_�捞�G���[���e�[�u��
        Dim strUploadDate As String                              '�A�b�v���[�h����
        Dim ninsyou As New NinsyouBC
        Dim strUserId As String                                  '���[�U�[ID

        '�A�b�v���[�h����
        'strUploadDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")
        strUploadDate = commonDA.SelSystemDate().Rows(0).Item(0).ToString
        '���[�U�[ID
        strUserId = ninsyou.GetUserID()
        'CSV�t�@�C����
        strNyuuryokuFileMei = CutMaxLength(fupId.FileName, 128)
        'EDI���쐬��
        Dim strEdiJouhouSakuseiDate As String = String.Empty
        '�G���[FLG
        Dim errorFlg As Integer = 0
        '���o�̓X�g���[��
        myStream = fupId.FileContent
        '�X�g���[�����[�_�[
        myReader = New IO.StreamReader(myStream, System.Text.Encoding.GetEncoding(932))
        '�̔����i�}�X�^���쐬����
        Call SetTSitenbetuTukiKeikakuKanri(dtTSitenbetuTukiKeikakuKanriOk)
        '�̔����i�G���[�e�[�u�����쐬����
        Call SetTSitenTukibetuTorikomiError(dtTSitenTukibetuTorikomiError)

        Try
            Do
                '�捞�t�@�C����ǂݍ���
                ReDim Preserve strCsvLine(intLineCount)
                strCsvLine(intLineCount) = myReader.ReadLine()
                intLineCount += 1
            Loop Until myReader.EndOfStream

            'EDI���쐬��
            'strEdiJouhouSakuseiDate = Right("0" & strCsvLine(0).Split(CChar(","))(1), 40)
            If strCsvLine(0).Split(CChar(",")).Length >= 2 Then
                strEdiJouhouSakuseiDate = strCsvLine(0).Split(CChar(","))(1)
            Else
                strEdiJouhouSakuseiDate = String.Empty
            End If

            '�c�Ƌ敪
            Dim strEigyouKbn As String = String.Empty
            Dim intFlg As Integer = 0
            '�ő匏���`�F�b�N
            If strCsvLine.Length > 3 Then
                '�G���[���b�Z�[�W��\�����āA�����I������
                Return String.Format(CommonMessage.MSG061E, "3")
            End If

            'CSV�t�@�C�����`�F�b�N
            For i As Integer = 0 To strCsvLine.Length - 1
                strReadLine = strCsvLine(i)
                If (Not strReadLine Is Nothing) AndAlso (Replace(strReadLine, ",", "") <> "") Then
                    'CSV��ރ`�F�b�N
                    If strReadLine.Split(CChar(","))(0) <> "A1" Then
                        '�G���[���b�Z�[�W��\�����āA�����I������
                        Return String.Format(CommonMessage.MSG056E, "�x�X���ʌv��l�捞CSV")
                    End If
                    If strReadLine.Split(CChar(",")).Length >= 4 Then
                        '�f�[�^���m�F�ς݃`�F�b�N
                        Dim intDataCount As Integer = sitenTukibetuKeikakuchiInputDA.SelTSitenbetuTukiKeikakuKanriCount(strReadLine.Split(CChar(","))(2), strReadLine.Split(CChar(","))(3))
                        If intDataCount > 0 Then
                            '�G���[���b�Z�[�W��\�����āA�����I������
                            Return CommonMessage.MSG064E
                        End If
                    Else
                        '�G���[�f�[�^���쐬����
                        Call SetTSitenTukibetuTorikomiErrorData(i + 1, "2", dtTSitenTukibetuTorikomiError)
                        errorFlg += 1
                        'Continue For
                    End If

                    '���ڐ��`�F�b�N
                    If strReadLine.Split(CChar(",")).Length > 42 Then
                        '�G���[�f�[�^���쐬����
                        Call SetTSitenTukibetuTorikomiErrorData(i + 1, "1", dtTSitenTukibetuTorikomiError)
                        errorFlg += 1
                        'Continue For
                    End If
                    If strReadLine.Split(CChar(",")).Length < 42 Then
                        strReadLine = strReadLine & StrDup(42 - strReadLine.Split(CChar(",")).Length, ",")
                    End If
                    '�K�{�`�F�b�N
                    If Not ChkNotNull(strReadLine) Then
                        '�G���[�f�[�^���쐬����
                        Call SetTSitenTukibetuTorikomiErrorData(i + 1, "2", dtTSitenTukibetuTorikomiError)
                        errorFlg += 1
                        'Continue For
                    End If
                    '�K�{����(�L�[����)�������ޑ��݃`�F�b�N
                    Dim intBusyoCdCount As Integer = sitenTukibetuKeikakuchiInputDA.SelMBusyoKanri(strReadLine.Split(CChar(","))(3))
                    If intBusyoCdCount = 0 Then
                        '�G���[�f�[�^���쐬����
                        Call SetTSitenTukibetuTorikomiErrorData(i + 1, "3", dtTSitenTukibetuTorikomiError)
                        errorFlg += 1
                        'Continue For
                    End If
                    '�K�{����(�L�[���ځj�c�Ƌ敪���u1�A3�A4�v�ȊO�̏ꍇ�A
                    If strReadLine.Split(CChar(","))(5) <> "1" AndAlso strReadLine.Split(CChar(","))(5) <> "3" AndAlso strReadLine.Split(CChar(","))(5) <> "4" Then
                        '�G���[�f�[�^���쐬����
                        Call SetTSitenTukibetuTorikomiErrorData(i + 1, "4", dtTSitenTukibetuTorikomiError)
                        errorFlg += 1
                        'Continue For
                    End If
                    
                    '�K�{����(�L�[���ځj�c�Ƌ敪���d���ȏꍇ
                    If intFlg = 1 AndAlso strEigyouKbn = strReadLine.Split(CChar(","))(5) Then
                        '�G���[�f�[�^���쐬����
                        Call SetTSitenTukibetuTorikomiErrorData(i + 1, "5", dtTSitenTukibetuTorikomiError)
                        errorFlg += 1
                        'Continue For
                    End If
                    If intFlg = 2 AndAlso (strEigyouKbn.Split(CChar(","))(0) = strReadLine.Split(CChar(","))(5) OrElse strEigyouKbn.Split(CChar(","))(1) = strReadLine.Split(CChar(","))(5)) Then
                        '�G���[�f�[�^���쐬����
                        Call SetTSitenTukibetuTorikomiErrorData(i + 1, "5", dtTSitenTukibetuTorikomiError)
                        errorFlg += 1
                        'Continue For
                    End If
                    '�֑������`�F�b�N
                    If Not ChkKinjiMoji(strReadLine) Then
                        '�G���[�f�[�^���쐬����
                        Call SetTSitenTukibetuTorikomiErrorData(i + 1, "6", dtTSitenTukibetuTorikomiError)
                        errorFlg += 1
                        'Continue For
                    End If
                    '���l�^���ڃ`�F�b�N
                    If Not ChkSuuti(strReadLine) Then
                        '�G���[�f�[�^���쐬����
                        Call SetTSitenTukibetuTorikomiErrorData(i + 1, "7", dtTSitenTukibetuTorikomiError)
                        errorFlg += 1
                        'Continue For
                    End If
                    '���ڍő咷�`�F�b�N
                    If Not ChkMaxLength(strReadLine) Then
                        '�G���[�f�[�^���쐬����
                        Call SetTSitenTukibetuTorikomiErrorData(i + 1, "8", dtTSitenTukibetuTorikomiError)
                        errorFlg += 1
                        'Continue For
                    End If
                    '�c�Ƌ敪
                    If strEigyouKbn = String.Empty AndAlso i = 0 Then
                        strEigyouKbn = strReadLine.Split(CChar(","))(5)
                    Else
                        strEigyouKbn = strEigyouKbn + "," + strReadLine.Split(CChar(","))(5)
                    End If
                    intFlg = intFlg + 1
                    '�G���[���Ȃ��ꍇ
                    If errorFlg = 0 Then
                        '�X�V��ǉ��𔻒f����
                        Call SetTSitenTukibetuTorikomiDataRow(strReadLine, dtTSitenbetuTukiKeikakuKanriOk)
                    End If

                End If
            Next
            '�G���[�L���t���O��ݒ肷��
            If dtTSitenTukibetuTorikomiError.Rows.Count > 0 Then
                strUmuFlg = "1"
                dtTSitenbetuTukiKeikakuKanriOk.Rows.Clear()
            End If
            'CSV�t�@�C�����捞
            If Not CsvFileUpload(dtTSitenbetuTukiKeikakuKanriOk, dtTSitenTukibetuTorikomiError, strUploadDate, strEdiJouhouSakuseiDate, strUserId, strNyuuryokuFileMei) Then

            End If
        Catch ex As Exception
        End Try

        Return String.Empty
    End Function

    ''' <summary>
    ''' �f�[�^�}������
    ''' </summary>
    ''' <param name="dtTSitenbetuTukiKeikakuKanriOk">�x�X�ʌ��ʌv��Ǘ��f�[�^</param>
    ''' <param name="dtTSitenTukibetuTorikomiError">�x�X�ʌ��ʌv��Ǘ��\_�捞�G���[�f�[�^</param>
    ''' <param name="strUploadDate">�A�b�v���[�h����</param>
    ''' <param name="strUserId">���[�U�[ID</param>
    ''' <param name="strNyuuryokuFileMei">�捞�t�@�C������</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CsvFileUpload(ByVal dtTSitenbetuTukiKeikakuKanriOk As Data.DataTable, _
                                  ByVal dtTSitenTukibetuTorikomiError As Data.DataTable, _
                                  ByVal strUploadDate As String, _
                                  ByVal strEdiJouhouSakuseiDate As String, _
                                  ByVal strUserId As String, _
                                  ByVal strNyuuryokuFileMei As String) As Boolean
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                               dtTSitenbetuTukiKeikakuKanriOk, _
                               dtTSitenTukibetuTorikomiError, _
                               strUploadDate, _
                               strEdiJouhouSakuseiDate, _
                               strUserId, _
                               strNyuuryokuFileMei)

        Using scope As New TransactionScope(TransactionScopeOption.RequiresNew)
            Try
                '�u�x�X�ʌ��ʌv��Ǘ��e�[�u���v�ɑ}������
                If dtTSitenbetuTukiKeikakuKanriOk.Rows.Count > 0 Then
                    'EXCEl�捞�f�[�^�}������
                    For i As Integer = 0 To dtTSitenbetuTukiKeikakuKanriOk.Rows.Count - 1
                        If Not sitenTukibetuKeikakuchiInputDA.InsTSitenbetuTukiKeikakuKanri(dtTSitenbetuTukiKeikakuKanriOk.Rows(i), strUserId) Then
                            Throw New ApplicationException
                        End If
                    Next
                End If
                '�x�X�ʌ��ʌv��Ǘ��\_�捞�G���[���e�[�u����o�^����
                If dtTSitenTukibetuTorikomiError.Rows.Count > 0 Then
                    For i As Integer = 0 To dtTSitenTukibetuTorikomiError.Rows.Count - 1
                        '�G���[�f�[�^�}������
                        If Not sitenTukibetuKeikakuchiInputDA.InsTSitenTukibetuTorikomiError(strUploadDate, strEdiJouhouSakuseiDate, dtTSitenTukibetuTorikomiError.Rows(i).Item(0).ToString, dtTSitenTukibetuTorikomiError.Rows(i).Item(1).ToString, strUserId) Then
                            '���s�̏ꍇ
                            scope.Dispose()
                        End If
                    Next
                End If
                '�A�b�v���[�h�Ǘ��e�[�u����o�^����
                If Not sitenTukibetuKeikakuchiInputDA.InsTUploadKanri(strUploadDate, strEdiJouhouSakuseiDate, CStr(IIf(dtTSitenTukibetuTorikomiError.Rows.Count > 0, 1, 0)), strNyuuryokuFileMei, strUserId) Then
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

    ''' <summary>
    ''' �ő咷��؂���
    ''' </summary>
    ''' <param name="strValue">�捞�t�@�C���ڍ�</param>
    ''' <param name="intMaxByteCount">"128"���Œ�</param>
    ''' <returns>�捞�t�@�C���̖���</returns>
    ''' <remarks>�捞�t�@�C���̖��̂��擾����</remarks>
    ''' <history>
    ''' <para>2012/12/05 P-44979 ���� �V�K�쐬 </para>
    ''' </history>
    Public Function CutMaxLength(ByVal strValue As String, ByVal intMaxByteCount As Integer) As String

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strValue, intMaxByteCount)

        Dim hEncoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

        Dim intLengthCount As Integer = 0
        For i As Integer = strValue.Length To 0 Step -1
            Dim btBytes As Byte() = hEncoding.GetBytes(Left(strValue, i))
            If btBytes.LongLength <= intMaxByteCount Then
                intLengthCount = i
                Exit For
            End If
        Next

        Return Left(strValue, intLengthCount)
    End Function

    ''' <summary>
    ''' ���ڍő咷�`�F�b�N
    ''' </summary>
    ''' <param name="strLine">����O�s</param>
    ''' <returns>TRUE�F�G���[�����AFALSE�F�G���[�L��</returns>
    ''' <remarks>�捞�f�[�^����O�s�e���ڍő咷�`�F�b�N</remarks>
    ''' <history>
    ''' <para>2012/12/05 P-44979 ���� �V�K�쐬 </para>
    ''' </history>
    Public Function ChkMaxLength(ByVal strLine As String) As Boolean

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strLine)

        Dim hEncoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

        'For i As Integer = 0 To strLine.Split(CChar(",")).Length - 1
        For i As Integer = 0 To 41
            Dim btBytes As Byte() = hEncoding.GetBytes(strLine.Split(CChar(","))(i))
            If btBytes.LongLength > SITENBETU_MAX_LENGTH(i) Then
                Return False
            End If
        Next

        Return True
    End Function

    ''' <summary>
    ''' �֑������`�F�b�N
    ''' </summary>
    ''' <param name="target">����O����</param>
    ''' <returns>TRUE�F�G���[�����AFALSE�F�G���[�L��</returns>
    ''' <remarks>�捞�f�[�^����O���ڋ֑������`�F�b�N</remarks>
    ''' <history>
    ''' <para>2012/12/05 P-44979 ���� �V�K�쐬 </para>
    ''' </history>
    Public Function ChkKinjiMoji(ByVal target As String) As Boolean

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, target)

        For Each st As String In arrayKinsiStr

            If target.IndexOf(st) >= 0 Then
                Return False
            End If

        Next

        Return True
    End Function

    ''' <summary>
    ''' �����`�F�b�N
    ''' </summary>
    ''' <param name="inTarget">����O����</param>
    ''' <returns>TRUE�F�G���[�����AFALSE�F�G���[�L��</returns>
    ''' <remarks>�捞�f�[�^����O���ڐ����`�F�b�N</remarks>
    ''' <history>
    ''' <para>2012/12/05 P-44979 ���� �V�K�쐬 </para>
    ''' </history>
    Function CheckHankaku(ByVal inTarget As String) As Boolean

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, inTarget)

        If inTarget.Length = System.Text.Encoding.Default.GetByteCount(inTarget) And IsNumeric(inTarget) Then
            Try
                Dim intTemp As Long = CLng(inTarget)
            Catch ex As Exception
                Return False
            End Try
            '*********************2013/03/09 �w�ENo.37 ���Ή�'*********************
            'If InStr(inTarget, ".") > 0 Or InStr(inTarget, "-") > 0 Or InStr(inTarget, "+") > 0 Then
            If InStr(inTarget, ".") > 0 Or InStr(inTarget, "+") > 0 Then
                Return False
            Else
                Return True
            End If
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' ���l�^���ڃ`�F�b�N
    ''' </summary>
    ''' <param name="strLine">�捞�f�[�^����O�s</param>
    ''' <returns>TRUE�F�G���[�����AFALSE�F�G���[�L��</returns>
    ''' <remarks>�捞�f�[�^����O�s���l�^���ڃ`�F�b�N</remarks>
    ''' <history>
    ''' <para>2012/12/05 P-44979 ���� �V�K�쐬 </para>
    ''' </history>
    Public Function ChkSuuti(ByVal strLine As String) As Boolean

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strLine)

        For Each i As Integer In HANBAI_NUM_INDEX

            If (Not strLine.Split(CChar(","))(i).Trim.Equals(String.Empty)) AndAlso (Not CheckHankaku(strLine.Split(CChar(","))(i))) Then
                Return False
            End If
        Next

        Return True
    End Function

    ''' <summary>
    ''' �K�{�`�F�b�N
    ''' </summary>
    ''' <param name="strLine">�捞�f�[�^����O�s</param>
    ''' <returns>TRUE�F�G���[�����AFALSE�F�G���[�L��</returns>
    ''' <remarks>�捞�f�[�^����O�s�K�{���݃`�F�b�N</remarks>
    ''' <history>
    ''' <para>2012/12/05 P-44979 ���� �V�K�쐬 </para>
    ''' </history>
    Public Function ChkNotNull(ByVal strLine As String) As Boolean

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strLine)

        For Each i As Integer In HANBAI_NOTNULL_INDEX

            If strLine.Split(CChar(","))(i).Trim.Equals(String.Empty) Then
                Return False
            End If
        Next

        Return True
    End Function

    ''' <summary>
    ''' �x�X�ʌ��ʌv��Ǘ��e�[�u�����쐬����
    ''' </summary>
    ''' <param name="dtTSitenbetuTukiKeikakuKanriOk">�x�X�ʌ��ʌv��Ǘ��e�[�u��</param>
    ''' <remarks>�x�X�ʌ��ʌv��Ǘ��e�[�u�����쐬����</remarks>
    ''' <history>
    ''' <para>2012/12/05 P-44979 ���� �V�K�쐬 </para>
    ''' </history>
    Public Sub SetTSitenbetuTukiKeikakuKanri(ByRef dtTSitenbetuTukiKeikakuKanriOk As Data.DataTable)

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, dtTSitenbetuTukiKeikakuKanriOk)

        Dim dc As Data.DataColumn

        dc = New Data.DataColumn
        dc.ColumnName = "keikaku_nendo"                '�v��_�N�x
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "busyo_cd"                     '��������
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "siten_mei"                    '�x�X��
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "eigyou_kbn"                   '�c�Ƌ敪
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "4gatu_keikaku_kensuu"         '4��_�v�挏��
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "4gatu_keikaku_kingaku"        '4��_�v����z
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "4gatu_keikaku_arari"          '4��_�v��e��
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "5gatu_keikaku_kensuu"         '5��_�v�挏��
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "5gatu_keikaku_kingaku"        '5��_�v����z
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "5gatu_keikaku_arari"          '5��_�v��e��
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "6gatu_keikaku_kensuu"         '6��_�v�挏��
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "6gatu_keikaku_kingaku"        '6��_�v����z
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "6gatu_keikaku_arari"          '6��_�v��e��
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "7gatu_keikaku_kensuu"         '7��_�v�挏��
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "7gatu_keikaku_kingaku"        '7��_�v����z
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "7gatu_keikaku_arari"          '7��_�v��e��
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "8gatu_keikaku_kensuu"         '8��_�v�挏��
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "8gatu_keikaku_kingaku"        '8��_�v����z
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "8gatu_keikaku_arari"          '8��_�v��e��
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "9gatu_keikaku_kensuu"         '9��_�v�挏��
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "9gatu_keikaku_kingaku"        '9��_�v����z
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "9gatu_keikaku_arari"          '9��_�v��e��
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "10gatu_keikaku_kensuu"        '10��_�v�挏��
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "10gatu_keikaku_kingaku"       '10��_�v����z
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "10gatu_keikaku_arari"         '10��_�v��e��
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "11gatu_keikaku_kensuu"        '11��_�v�挏��
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "11gatu_keikaku_kingaku"       '11��_�v����z
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "11gatu_keikaku_arari"         '11��_�v��e��
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "12gatu_keikaku_kensuu"        '12��_�v�挏��
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "12gatu_keikaku_kingaku"       '12��_�v����z
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "12gatu_keikaku_arari"         '12��_�v��e��
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "1gatu_keikaku_kensuu"         '1��_�v�挏��
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "1gatu_keikaku_kingaku"        '1��_�v����z
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "1gatu_keikaku_arari"          '1��_�v��e��
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "2gatu_keikaku_kensuu"         '2��_�v�挏��
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "2gatu_keikaku_kingaku"        '2��_�v����z
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "2gatu_keikaku_arari"          '2��_�v��e��
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "3gatu_keikaku_kensuu"         '3��_�v�挏��
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "3gatu_keikaku_kingaku"        '3��_�v����z
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "3gatu_keikaku_arari"          '3��_�v��e��
        dtTSitenbetuTukiKeikakuKanriOk.Columns.Add(dc)

    End Sub

    ''' <summary>
    ''' �x�X�ʌ��ʌv��Ǘ��\_�捞�G���[���e�[�u�����쐬����
    ''' </summary>
    ''' <param name="dtTSitenTukibetuTorikomiError">�x�X�ʌ��ʌv��Ǘ��\_�捞�G���[���e�[�u��</param>
    ''' <remarks>�x�X�ʌ��ʌv��Ǘ��\_�捞�G���[���e�[�u�����쐬����</remarks>
    ''' <history>
    ''' <para>2012/12/05 P-44979 ���� �V�K�쐬 </para>
    ''' </history>
    Public Sub SetTSitenTukibetuTorikomiError(ByRef dtTSitenTukibetuTorikomiError As Data.DataTable)
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, dtTSitenTukibetuTorikomiError)

        Dim dc As Data.DataColumn

        dc = New Data.DataColumn
        dc.ColumnName = "gyou_no"                '�sNo
        dtTSitenTukibetuTorikomiError.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "kbn"                    '�敪
        dtTSitenTukibetuTorikomiError.Columns.Add(dc)

    End Sub

    ''' <summary>
    ''' �x�X�ʌ��ʌv��Ǘ��\_�捞�G���[���e�[�u�����쐬����
    ''' </summary>
    ''' <param name="intLineNo">�捞�f�[�^����O�s</param>
    ''' <param name="strKbn">�G���[�敪</param>
    ''' <param name="dtTSitenTukibetuTorikomiError">�x�X�ʌ��ʌv��Ǘ��\_�捞�G���[���e�[�u��</param>
    ''' <remarks>�x�X�ʌ��ʌv��Ǘ��\_�捞�G���[���e�[�u�����쐬����</remarks>
    ''' <history>
    ''' <para>2012/12/05 P-44979 ���� �V�K�쐬 </para>
    ''' </history>
    Private Sub SetTSitenTukibetuTorikomiErrorData(ByVal intLineNo As Integer, _
                                                   ByVal strKbn As String, _
                                                   ByRef dtTSitenTukibetuTorikomiError As Data.DataTable)
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, intLineNo, strKbn, dtTSitenTukibetuTorikomiError)

        Dim dr As Data.DataRow
        dr = dtTSitenTukibetuTorikomiError.NewRow
        dr.Item("gyou_no") = intLineNo           '�v��_�N�x
        dr.Item("kbn") = strKbn                  '�G���[�敪

        dtTSitenTukibetuTorikomiError.Rows.Add(dr)

    End Sub

    ''' <summary>�x�X�ʌ��ʌv��Ǘ��f�[�^���쐬����</summary>
    ''' <param name="strLine">CSV�t�@�C���̊Y���s�̃f�[�^</param>
    ''' <param name="dtTSitenbetuTukiKeikakuKanriOk">�x�X�ʌ��ʌv��Ǘ��f�[�^</param>
    ''' <history>
    ''' <para>2012/12/05 P-44979 ���� �V�K�쐬 </para>
    ''' </history>
    Public Sub SetTSitenTukibetuTorikomiDataRow(ByVal strLine As String, _
                                                ByRef dtTSitenbetuTukiKeikakuKanriOk As Data.DataTable)
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strLine, dtTSitenbetuTukiKeikakuKanriOk)

        Dim dr As Data.DataRow
        dr = dtTSitenbetuTukiKeikakuKanriOk.NewRow
        dr.Item("keikaku_nendo") = strLine.Split(CChar(","))(2).Trim           '�v��_�N�x
        dr.Item("busyo_cd") = strLine.Split(CChar(","))(3).Trim                '��������
        dr.Item("siten_mei") = strLine.Split(CChar(","))(4).Trim               '�x�X��

        dr.Item("eigyou_kbn") = strLine.Split(CChar(","))(5).Trim              '�c�Ƌ敪

        dr.Item("4gatu_keikaku_kensuu") = strLine.Split(CChar(","))(6).Trim    '4��_�v�挏��
        dr.Item("4gatu_keikaku_kingaku") = strLine.Split(CChar(","))(7).Trim   '4��_�v����z
        dr.Item("4gatu_keikaku_arari") = strLine.Split(CChar(","))(8).Trim     '4��_�v����z
        dr.Item("5gatu_keikaku_kensuu") = strLine.Split(CChar(","))(9).Trim    '5��_�v����z
        dr.Item("5gatu_keikaku_kingaku") = strLine.Split(CChar(","))(10).Trim  '5��_�v����z
        dr.Item("5gatu_keikaku_arari") = strLine.Split(CChar(","))(11).Trim    '5��_�v��e��
        dr.Item("6gatu_keikaku_kensuu") = strLine.Split(CChar(","))(12).Trim   '6��_�v�挏��
        dr.Item("6gatu_keikaku_kingaku") = strLine.Split(CChar(","))(13).Trim  '6��_�v����z
        dr.Item("6gatu_keikaku_arari") = strLine.Split(CChar(","))(14).Trim    '6��_�v����z

        dr.Item("7gatu_keikaku_kensuu") = strLine.Split(CChar(","))(15).Trim   '7��_�v����z
        dr.Item("7gatu_keikaku_kingaku") = strLine.Split(CChar(","))(16).Trim  '7��_�v����z
        dr.Item("7gatu_keikaku_arari") = strLine.Split(CChar(","))(17).Trim    '7��_�v��e��
        dr.Item("8gatu_keikaku_kensuu") = strLine.Split(CChar(","))(18).Trim   '8��_�v�挏��
        dr.Item("8gatu_keikaku_kingaku") = strLine.Split(CChar(","))(19).Trim  '8��_�v����z
        dr.Item("8gatu_keikaku_arari") = strLine.Split(CChar(","))(20).Trim    '8��_�v����z
        dr.Item("9gatu_keikaku_kensuu") = strLine.Split(CChar(","))(21).Trim   '9��_�v����z
        dr.Item("9gatu_keikaku_kingaku") = strLine.Split(CChar(","))(22).Trim  '9��_�v����z
        dr.Item("9gatu_keikaku_arari") = strLine.Split(CChar(","))(23).Trim    '9��_�v��e��

        dr.Item("10gatu_keikaku_kensuu") = strLine.Split(CChar(","))(24).Trim  '10��_�v�挏��
        dr.Item("10gatu_keikaku_kingaku") = strLine.Split(CChar(","))(25).Trim '10��_�v����z
        dr.Item("10gatu_keikaku_arari") = strLine.Split(CChar(","))(26).Trim   '10��_�v����z
        dr.Item("11gatu_keikaku_kensuu") = strLine.Split(CChar(","))(27).Trim  '11��_�v����z
        dr.Item("11gatu_keikaku_kingaku") = strLine.Split(CChar(","))(28).Trim '11��_�v����z
        dr.Item("11gatu_keikaku_arari") = strLine.Split(CChar(","))(29).Trim   '11��_�v��e��
        dr.Item("12gatu_keikaku_kensuu") = strLine.Split(CChar(","))(30).Trim  '12��_�v�挏��
        dr.Item("12gatu_keikaku_kingaku") = strLine.Split(CChar(","))(31).Trim '12��_�v����z
        dr.Item("12gatu_keikaku_arari") = strLine.Split(CChar(","))(32).Trim   '12��_�v����z

        dr.Item("1gatu_keikaku_kensuu") = strLine.Split(CChar(","))(33).Trim   '1��_�v����z
        dr.Item("1gatu_keikaku_kingaku") = strLine.Split(CChar(","))(34).Trim  '1��_�v����z
        dr.Item("1gatu_keikaku_arari") = strLine.Split(CChar(","))(35).Trim    '1��_�v��e��
        dr.Item("2gatu_keikaku_kensuu") = strLine.Split(CChar(","))(36).Trim   '2��_�v�挏��
        dr.Item("2gatu_keikaku_kingaku") = strLine.Split(CChar(","))(37).Trim  '2��_�v����z
        dr.Item("2gatu_keikaku_arari") = strLine.Split(CChar(","))(38).Trim    '2��_�v����z
        dr.Item("3gatu_keikaku_kensuu") = strLine.Split(CChar(","))(39).Trim   '3��_�v����z
        dr.Item("3gatu_keikaku_kingaku") = strLine.Split(CChar(","))(40).Trim  '3��_�v����z
        dr.Item("3gatu_keikaku_arari") = strLine.Split(CChar(","))(41).Trim    '3��_�v��e��
        dtTSitenbetuTukiKeikakuKanriOk.Rows.Add(dr)

    End Sub

End Class
