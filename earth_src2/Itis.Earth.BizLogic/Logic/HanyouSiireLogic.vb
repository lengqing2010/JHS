Imports System.Transactions
Imports Itis.Earth.DataAccess

Public Class HanyouSiireLogic

    '�ėp�d���f�[�^�捞�C���X�^���X���� 
    Private hanyouSiireDA As New HanyouSiireDataAccess

    ''' <summary>�A�b�v���[�h�Ǘ����擾����</summary>
    ''' <returns>�A�b�v���[�h�Ǘ��f�[�^�e�[�u��</returns>
    Public Function GetUploadKanri() As Data.DataTable
        Return hanyouSiireDA.SelUploadKanri()
    End Function

    ''' <summary>�A�b�v���[�h�Ǘ��̌������擾����</summary>
    ''' <returns>�A�b�v���[�h�Ǘ��̌���</returns>
    Public Function GetUploadKanriCount() As Integer
        Return hanyouSiireDA.SelUploadKanriCount()
    End Function

    ''' <summary>�ėp�d���G���[�����擾����</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI���쐬��</param>
    ''' <param name="strSyoriDate">��������</param>
    ''' <param name="intTopCount">�擾�̍ő�s��</param>
    ''' <returns>�ėp�d���G���[�f�[�^�e�[�u��</returns>
    Public Function GetHanyouSiireErr(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoriDate As String, ByVal intTopCount As Integer) As DataTable
        Return hanyouSiireDA.SelHanyouSiireErr(strEdiJouhouSakuseiDate, strSyoriDate, intTopCount)
    End Function

    ''' <summary>�ėp�d���G���[�������擾����</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI���쐬��</param>
    ''' <param name="strSyoriDate">��������</param>
    ''' <returns>�ėp�d���G���[����</returns>
    Public Function GetHanyouSiireErrCount(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoriDate As String) As Integer
        Return hanyouSiireDA.SelHanyouSiireErrCount(strEdiJouhouSakuseiDate, strSyoriDate)
    End Function

    Public Function GetUploadKanri(ByVal strFileName As String) As Integer
        Return hanyouSiireDA.SelUploadKanri("7", strFileName)
    End Function
 
    Public Function ChkCsvFile(ByVal strCsvLine() As String, ByVal FileName As String, ByRef strUmuFlg As String) As String


        Dim strReadLine As String                               '�捞�t�@�C���Ǎ��ݍs


        Dim strNyuuryokuFileMei As String                       'CSV�t�@�C����
        Dim strEdiJouhouSakuseiDate As String = String.Empty    'EDI���쐬��
        Dim dtHanyouSiireOk As New Data.DataTable               '�ėp�d���}�X�^
        Dim dtHanyouSiireErr As New Data.DataTable              '�ėp�d���G���[
        Dim strUploadDate As String                             '�A�b�v���[�h����
        Dim ninsyou As New Ninsyou()
        Dim strUserId As String                                 '���[�U�[ID
        Dim strMaxUpload As String                              'CSV�捞�̏������
        Dim commonCheck As New CsvInputCheck
        '�A�b�v���[�h����
        strUploadDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")
        '���[�U�[ID
        strUserId = ninsyou.GetUserID()
        'CSV�t�@�C����
        strNyuuryokuFileMei = commonCheck.CutMaxLength(FileName, 128)
        'CSV�捞�̏������
        strMaxUpload = System.Configuration.ConfigurationManager.AppSettings("CsvInputMaxLineCount").ToString

        '�H�����i�}�X�^���쐬����
        Call SetHanyouSiireOk(dtHanyouSiireOk)
        '�H�����i�G���[�e�[�u�����쐬����
        Call SetHanyouSiireErr(dtHanyouSiireErr)

        Try
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
            strEdiJouhouSakuseiDate = Right("          " & strCsvLine(1).Split(",")(0), 40)
            strEdiJouhouSakuseiDate = Replace(strEdiJouhouSakuseiDate, "/", "")
            strEdiJouhouSakuseiDate = Replace(strEdiJouhouSakuseiDate, ":", "")
            strEdiJouhouSakuseiDate = Replace(strEdiJouhouSakuseiDate, " ", "")
            strEdiJouhouSakuseiDate = Right(strEdiJouhouSakuseiDate, 40)
            'CSV�t�@�C�����`�F�b�N
            For i As Integer = 1 To strCsvLine.Length - 1
                strReadLine = strCsvLine(i)
                Dim arrLine2() As String = Split(strReadLine, ",")
                arrLine2(0) = Replace(arrLine2(0), "/", "")
                arrLine2(0) = Replace(arrLine2(0), ":", "")
                arrLine2(0) = Replace(arrLine2(0), " ", "")
                arrLine2(0) = Right(arrLine2(0), 40)
                arrLine2(5) = Right("0000" & arrLine2(5).Trim, 4)
                arrLine2(6) = Right("00" & arrLine2(6).Trim, 2)
                '===============2013/07/04 �ԗ� �C����=========================
                'arrLine2(7) = Right("00000" & arrLine2(7).Trim, 5)
                If (arrLine2(7).Trim.Equals(String.Empty)) OrElse (arrLine2(7).Trim.ToUpper.Equals("NULL")) Then
                    arrLine2(7) = String.Empty
                Else
                    arrLine2(7) = Right("00000" & arrLine2(7).Trim, 5)
                End If
                '===============2013/07/04 �ԗ� �C����=========================
                strReadLine = String.Join(",", arrLine2)
                If (Not strReadLine Is Nothing) AndAlso (Replace(strReadLine, ",", "") <> "") Then
                    ''EDI���쐬�����O0���ߕϊ�
                    'If strReadLine.Split(",")(0).Length < 10 Then
                    '    Dim arrLine() As String = Split(strReadLine, ",")
                    '    arrLine(0) = Right("          " & arrLine(0).Trim, 10)
                    '    strReadLine = String.Join(",", arrLine)
                    'End If

                    '�������ߕϊ�
                    If strReadLine.Split(",").Length < CsvInputCheck.SIIRE_FIELD_COUNT Then
                        strReadLine = strReadLine & StrDup(CsvInputCheck.SIIRE_FIELD_COUNT - strReadLine.Split(",").Length, ",")
                    End If
                    '�{�喼���擾
                    If strReadLine.Split(",")(17).Trim = "" Then
                        Dim arrLine() As String = Split(strReadLine, ",")
                        arrLine(17) = hanyouSiireDA.SelSesyuMei(strReadLine.Split(",")(15).Trim, strReadLine.Split(",")(16).Trim)
                        strReadLine = String.Join(",", arrLine)
                    End If
                    '�t�B�[���h���`�F�b�N
                    If Not commonCheck.ChkFieldCount(strReadLine.Split(",").Length, CsvInputCheck.SIIRE) Then
                        '�G���[�f�[�^���쐬����
                        Call SetHanyouSiireErrData(i + 1, strReadLine, dtHanyouSiireErr)
                        Continue For
                    End If
                    '���ڍő咷�`�F�b�N
                    If Not commonCheck.ChkMaxLength(strReadLine, CsvInputCheck.SIIRE) Then
                        '�G���[�f�[�^���쐬����
                        Call SetHanyouSiireErrData(i + 1, strReadLine, dtHanyouSiireErr)
                        Continue For
                    End If
                    '�֑������`�F�b�N
                    If Not commonCheck.ChkKinjiMoji(strReadLine) Then
                        '�G���[�f�[�^���쐬����
                        Call SetHanyouSiireErrData(i + 1, strReadLine, dtHanyouSiireErr)
                        Continue For
                    End If
                    '���l�^���ڃ`�F�b�N
                    If Not commonCheck.ChkSuuti(strReadLine, CsvInputCheck.SIIRE) Then
                        '�G���[�f�[�^���쐬����
                        Call SetHanyouSiireErrData(i + 1, strReadLine, dtHanyouSiireErr)
                        Continue For
                    End If
                    '�K�{�`�F�b�N
                    If Not commonCheck.ChkNotNull(strReadLine, CsvInputCheck.SIIRE) Then
                        '�G���[�f�[�^���쐬����
                        Call SetHanyouSiireErrData(i + 1, strReadLine, dtHanyouSiireErr)
                        Continue For
                    End If

                    '���t�`�F�b�N
                    If Not commonCheck.ChkNotDate(strReadLine, CsvInputCheck.SIIRE) Then
                        '�G���[�f�[�^���쐬����
                        Call SetHanyouSiireErrData(i + 1, strReadLine, dtHanyouSiireErr)
                        Continue For
                    End If


                    '���݃`�F�b�N()
                    If Not ChkMstSonZai(strReadLine) Then
                        '�G���[�f�[�^���쐬����
                        Call SetHanyouSiireErrData(i + 1, strReadLine, dtHanyouSiireErr)
                        Continue For
                    End If

                    '�s�ǉ�
                    Call SetHanyouSiireDataRow(strReadLine, dtHanyouSiireOk)
                End If
            Next
            '�G���[�L���t���O��ݒ肷��
            If dtHanyouSiireErr.Rows.Count > 0 Then
                strUmuFlg = "1"
            End If
            'CSV�t�@�C�����捞
            If Not CsvFileUpload(dtHanyouSiireOk, dtHanyouSiireErr, strUploadDate, strUserId, strNyuuryokuFileMei, strEdiJouhouSakuseiDate) Then
                Return Messages.Instance.MSG050E
            End If
        Catch ex As Exception
            Return Messages.Instance.MSG049E
        End Try

        Return String.Empty
    End Function
    ''' <summary>���݃`�F�b�N</summary>
    Private Function ChkMstSonZai(ByVal intFieldCount As String) As Boolean
        Dim genkaMasterDA As New GenkaMasterDataAccess
        '������Ёi������ЃR�[�h�E���Ə��R�[�h�j
        If Not genkaMasterDA.SelTyousaKaisyaMeiInput(intFieldCount.Split(",")(5).Trim, intFieldCount.Split(",")(6).Trim) Then
            Return False
        End If

        '�����X(�����X�R�[�h)
        If intFieldCount.Split(",")(7).Trim <> "" Then
            If Not hanyouSiireDA.SelKameitenInput(intFieldCount.Split(",")(7).Trim) Then
                Return False
            End If
        End If

        '���i�R�[�h
        If Not genkaMasterDA.SelSyouhinCdInput(intFieldCount.Split(",")(8).Trim) Then
            Return False
        End If

        Return True
    End Function
    ''' <summary>�ėp�d���e�[�u�����쐬����</summary>
    ''' <param name="dtHanyouSiireOk">�ėp�d���e�[�u��</param>
    Public Sub SetHanyouSiireOk(ByRef dtHanyouSiireOk As Data.DataTable)
        dtHanyouSiireOk.Columns.Add("torikesi")                     '���
        dtHanyouSiireOk.Columns.Add("tekiyou")                      '�E�v
        dtHanyouSiireOk.Columns.Add("siire_date")                   '�d���N����
        dtHanyouSiireOk.Columns.Add("denpyou_siire_date")           '�`�[�d���N����
        dtHanyouSiireOk.Columns.Add("tys_kaisya_cd")                '������ЃR�[�h		
        dtHanyouSiireOk.Columns.Add("tys_kaisya_jigyousyo_cd")      '������Ў��Ə��R�[�h	
        dtHanyouSiireOk.Columns.Add("kameiten_cd")                  '�����X�R�[�h		
        dtHanyouSiireOk.Columns.Add("syouhin_cd")                   '���i�R�[�h		
        dtHanyouSiireOk.Columns.Add("suu")                          '����			
        dtHanyouSiireOk.Columns.Add("tanka")                        '�P��
        dtHanyouSiireOk.Columns.Add("zei_kbn")                      '�ŋ敪			
        dtHanyouSiireOk.Columns.Add("syouhizei_gaku")               '����Ŋz		
        dtHanyouSiireOk.Columns.Add("siire_keijyou_flg")            '�d������FLG(����v��FLG)	
        dtHanyouSiireOk.Columns.Add("siire_keijyou_date")           '�d��������(����v���)	
        dtHanyouSiireOk.Columns.Add("kbn")                          '�敪
        dtHanyouSiireOk.Columns.Add("bangou")                       '�ԍ�
        dtHanyouSiireOk.Columns.Add("sesyu_mei")                    '�{�喼
    End Sub

    ''' <summary>�ėp�d���G���[�e�[�u�����쐬����</summary>
    ''' <param name="dtHanyouSiireErr">�ėp�d���G���[�e�[�u��</param>
    Public Sub SetHanyouSiireErr(ByRef dtHanyouSiireErr As Data.DataTable)
        dtHanyouSiireErr.Columns.Add("edi_jouhou_sakusei_date")      'EDI���쐬��
        dtHanyouSiireErr.Columns.Add("torikesi")                     '���
        dtHanyouSiireErr.Columns.Add("tekiyou")                      '�E�v
        dtHanyouSiireErr.Columns.Add("siire_date")                   '�d���N����
        dtHanyouSiireErr.Columns.Add("denpyou_siire_date")           '�`�[�d���N����
        dtHanyouSiireErr.Columns.Add("tys_kaisya_cd")                '������ЃR�[�h		
        dtHanyouSiireErr.Columns.Add("tys_kaisya_jigyousyo_cd")      '������Ў��Ə��R�[�h
        dtHanyouSiireErr.Columns.Add("kameiten_cd")                  '�����X�R�[�h
        dtHanyouSiireErr.Columns.Add("syouhin_cd")                   '���i�R�[�h
        dtHanyouSiireErr.Columns.Add("suu")                          '����			
        dtHanyouSiireErr.Columns.Add("tanka")                        '�P��
        dtHanyouSiireErr.Columns.Add("zei_kbn")                      '�ŋ敪			
        dtHanyouSiireErr.Columns.Add("syouhizei_gaku")               '����Ŋz	
        dtHanyouSiireErr.Columns.Add("siire_keijyou_flg")            '�d������FLG(����v��FLG)	
        dtHanyouSiireErr.Columns.Add("siire_keijyou_date")           '�d��������(����v���)	
        dtHanyouSiireErr.Columns.Add("kbn")                          '�敪
        dtHanyouSiireErr.Columns.Add("bangou")                       '�ԍ�
        dtHanyouSiireErr.Columns.Add("sesyu_mei")                    '�{�喼
        dtHanyouSiireErr.Columns.Add("gyou_no")                      '�sNO
    End Sub

    ''' <summary>�ėp�d���G���[�f�[�^���쐬����</summary>
    ''' <param name="intLineNo">CSV�t�@�C���̊Y���s�̍sNO</param>
    ''' <param name="strLine">CSV�t�@�C���̊Y���s�̃f�[�^</param>
    ''' <param name="dtHanyouSiireError">�ėp�d���G���[�f�[�^</param>
    Private Sub SetHanyouSiireErrData(ByVal intLineNo As Integer, _
                                       ByVal strLine As String, _
                                       ByRef dtHanyouSiireError As Data.DataTable)
        Dim intMaxCount As Integer
        Dim commonCheck As New CsvInputCheck
        Dim dr As Data.DataRow
        dr = dtHanyouSiireError.NewRow

        If strLine.Split(",").Length < CsvInputCheck.SIIRE_FIELD_COUNT Then
            intMaxCount = strLine.Split(",").Length
        Else
            intMaxCount = CsvInputCheck.SIIRE_FIELD_COUNT
        End If

        For i As Integer = 0 To intMaxCount - 1
            '�ő咷��؂���
            dr.Item(i) = commonCheck.CutMaxLength(strLine.Split(",")(i).Trim, commonCheck.SIIRE_MAX_LENGTH(i))
        Next
        '�s��
        dr.Item(CsvInputCheck.SIIRE_FIELD_COUNT) = CStr(intLineNo)

        dtHanyouSiireError.Rows.Add(dr)

    End Sub

    ''' <summary>�ėp�d���f�[�^���쐬����</summary>
    ''' <param name="strLine">CSV�t�@�C���̊Y���s�̃f�[�^</param>
    ''' <param name="dtHanyouSiireOk">�ėp�d���f�[�^</param>
    Public Sub SetHanyouSiireDataRow(ByVal strLine As String, _
                                     ByRef dtHanyouSiireOk As Data.DataTable)
        Dim dr As Data.DataRow
        dr = dtHanyouSiireOk.NewRow

     
        dr.Item("torikesi") = strLine.Split(",")(1).Trim
        dr.Item("tekiyou") = strLine.Split(",")(2).Trim                     '�E�v
        dr.Item("siire_date") = strLine.Split(",")(3).Trim                  '�d���N����
        dr.Item("denpyou_siire_date") = strLine.Split(",")(4).Trim          '�`�[�d���N����
        dr.Item("tys_kaisya_cd") = strLine.Split(",")(5).Trim               '������ЃR�[�h
        dr.Item("tys_kaisya_jigyousyo_cd") = strLine.Split(",")(6).Trim     '������Ў��Ə��R�[�h
        dr.Item("kameiten_cd") = strLine.Split(",")(7).Trim                 '�����X�R�[�h
        dr.Item("syouhin_cd") = strLine.Split(",")(8).Trim                  '���i�R�[�h
        dr.Item("suu") = strLine.Split(",")(9).Trim                         '����
        dr.Item("tanka") = strLine.Split(",")(10).Trim                      '�P��
        dr.Item("zei_kbn") = strLine.Split(",")(11).Trim                    '�ŋ敪
        dr.Item("syouhizei_gaku") = strLine.Split(",")(12).Trim             '����Ŋz
        dr.Item("siire_keijyou_flg") = strLine.Split(",")(13).Trim          '�d������FLG
        dr.Item("siire_keijyou_date") = strLine.Split(",")(14).Trim         '�d��������
        dr.Item("kbn") = strLine.Split(",")(15).Trim                        '�敪
        dr.Item("bangou") = strLine.Split(",")(16).Trim                     '�ԍ�
        dr.Item("sesyu_mei") = strLine.Split(",")(17).Trim                  '�{�喼

        dtHanyouSiireOk.Rows.Add(dr)
    End Sub

    ''' <summary>CSV�t�@�C�����捞����</summary>
    ''' <param name="dtHanyouSiireOk">�ėp�d���f�[�^</param>
    ''' <param name="dtHanyouSiireErr">�ėp�d���G���[�f�[�^</param>
    ''' <param name="strUserId">���[�U�[ID</param>
    ''' <param name="strNyuuryokuFileMei">���̓t�@�C����</param>
    ''' <param name="strEdiJouhouSakuseiDate">EDI���쐬��</param>
    ''' <returns>�����E���s�敪</returns>
    Public Function CsvFileUpload(ByVal dtHanyouSiireOk As Data.DataTable, _
                                  ByVal dtHanyouSiireErr As Data.DataTable, _
                                  ByVal strUploadDate As String, _
                                  ByVal strUserId As String, _
                                  ByVal strNyuuryokuFileMei As String, _
                                  ByVal strEdiJouhouSakuseiDate As String) As Boolean

        Using scope As New TransactionScope(TransactionScopeOption.RequiresNew)
            Try
                '�ėp�d���}�X�^��o�^����
                If dtHanyouSiireOk.Rows.Count > 0 Then
                    If Not hanyouSiireDA.InsHanyouSiire(dtHanyouSiireOk, strUserId) Then
                        Throw New ApplicationException
                    End If
                End If
                '�ėp�d���G���[���e�[�u����o�^����
                If dtHanyouSiireErr.Rows.Count > 0 Then
                    If Not hanyouSiireDA.InsHanyouSiireErr(dtHanyouSiireErr, strUploadDate, strUserId) Then
                        Throw New ApplicationException
                    End If
                End If
                '�A�b�v���[�h�Ǘ��e�[�u����o�^����
                If Not hanyouSiireDA.InsUploadKanri(strUploadDate, strNyuuryokuFileMei, strEdiJouhouSakuseiDate, CInt(IIf(dtHanyouSiireErr.Rows.Count, 1, 0)), strUserId) Then
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

End Class
