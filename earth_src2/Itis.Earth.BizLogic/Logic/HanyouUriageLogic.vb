Imports System.Transactions
Imports Itis.Earth.DataAccess

Public Class HanyouUriageLogic

    '�ėp����f�[�^�捞�C���X�^���X���� 
    Private hanyouUriageDA As New HanyouUriageDataAccess

    ''' <summary>�A�b�v���[�h�Ǘ����擾����</summary>
    ''' <returns>�A�b�v���[�h�Ǘ��f�[�^�e�[�u��</returns>
    Public Function GetUploadKanri() As Data.DataTable
        Return hanyouUriageDA.SelUploadKanri()
    End Function

    ''' <summary>�A�b�v���[�h�Ǘ��̌������擾����</summary>
    ''' <returns>�A�b�v���[�h�Ǘ��̌���</returns>
    Public Function GetUploadKanriCount() As Integer
        Return hanyouUriageDA.SelUploadKanriCount()
    End Function

    ''' <summary>�ėp����G���[�����擾����</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI���쐬��</param>
    ''' <param name="strSyoriDate">��������</param>
    ''' <param name="intTopCount">�擾�̍ő�s��</param>
    ''' <returns>�ėp����G���[�f�[�^�e�[�u��</returns>
    Public Function GetHanyouUriageErr(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoriDate As String, ByVal intTopCount As Integer) As DataTable
        Return hanyouUriageDA.SelHanyouUriageErr(strEdiJouhouSakuseiDate, strSyoriDate, intTopCount)
    End Function

    ''' <summary>�ėp����G���[�������擾����</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI���쐬��</param>
    ''' <param name="strSyoriDate">��������</param>
    ''' <returns>�ėp����G���[����</returns>
    Public Function GetHanyouUriageErrCount(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoriDate As String) As Integer
        Return hanyouUriageDA.SelHanyouUriageErrCount(strEdiJouhouSakuseiDate, strSyoriDate)
    End Function

    Public Function GetUploadKanri(ByVal strFileName As String) As Integer
        Dim HanyouSiireDataAccess As New HanyouSiireDataAccess
        Return HanyouSiireDataAccess.SelUploadKanri("6", strFileName)
    End Function

    Public Function ChkCsvFile(ByVal strCsvLine() As String, ByVal FileName As String, ByRef strUmuFlg As String) As String

        Dim strReadLine As String                               '�捞�t�@�C���Ǎ��ݍs


        Dim strNyuuryokuFileMei As String                       'CSV�t�@�C����
        Dim strEdiJouhouSakuseiDate As String = String.Empty    'EDI���쐬��
        Dim dtHanyouUriageOk As New Data.DataTable               '�ėp�d���}�X�^
        Dim dtHanyouUriageErr As New Data.DataTable              '�ėp�d���G���[
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
        Call SetHanyouUriageOk(dtHanyouUriageOk)
        '�H�����i�G���[�e�[�u�����쐬����
        Call SetHanyouUriageErr(dtHanyouUriageErr)

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
                '==========��2015/11/19 ������ add����X�敪,����X����  �C����======================
                'If arrLine2(8).Trim = "0" Then
                '    arrLine2(6) = Right("00000" & arrLine2(6).Trim, 5)
                'End If
                'If arrLine2(8).Trim = "1" Then
                '    arrLine2(6) = Right("0000" & arrLine2(6).Trim, 4)
                'End If
                'arrLine2(7) = Right("00" & arrLine2(7).Trim, 2)
                If arrLine2(6).Trim = "0" Then '����X�敪��0�̏ꍇ
                    arrLine2(7) = Right("00000" & arrLine2(7).Trim, 5) '����X����:�O0����5���ϊ�
                End If
                If arrLine2(6).Trim = "1" Then '����X�敪��1�̏ꍇ
                    arrLine2(7) = Right("000000" & arrLine2(7).Trim, 6) '����X����:�O0����6���ϊ�
                End If
                If arrLine2(10).Trim = "0" Then '������敪��0�̏ꍇ
                    arrLine2(8) = Right("00000" & arrLine2(8).Trim, 5) '�����溰��:�O0����5���ϊ�
                End If
                If arrLine2(10).Trim = "1" Then '������敪��1�̏ꍇ
                    arrLine2(8) = Right("0000" & arrLine2(8).Trim, 4) '�����溰��:�O0����4���ϊ�
                End If
                arrLine2(9) = Right("00" & arrLine2(9).Trim, 2) '������}��:�O0����2���ϊ�
                '==========��2015/11/19 ������ add����X�敪,����X����  �C����======================              
                strReadLine = String.Join(",", arrLine2)
                If (Not strReadLine Is Nothing) AndAlso (Replace(strReadLine, ",", "") <> "") Then
                    'EDI���쐬�����O0���ߕϊ�
                    If strReadLine.Split(",")(0).Length < 10 Then
                        Dim arrLine() As String = Split(strReadLine, ",")
                        arrLine(0) = Right("          " & arrLine(0).Trim, 10)
                        strReadLine = String.Join(",", arrLine)
                    End If
                    '�������ߕϊ�
                    If strReadLine.Split(",").Length < CsvInputCheck.URIAGE_FIELD_COUNT Then
                        strReadLine = strReadLine & StrDup(CsvInputCheck.URIAGE_FIELD_COUNT - strReadLine.Split(",").Length, ",")
                    End If
                    '�t�B�[���h���`�F�b�N
                    If Not commonCheck.ChkFieldCount(strReadLine.Split(",").Length, CsvInputCheck.URIAGE) Then
                        '�G���[�f�[�^���쐬����
                        Call SetHanyouUriageErrData(i + 1, strReadLine, dtHanyouUriageErr)
                        Continue For
                    End If
                    '���ڍő咷�`�F�b�N
                    If Not commonCheck.ChkMaxLength(strReadLine, CsvInputCheck.URIAGE) Then
                        '�G���[�f�[�^���쐬����
                        Call SetHanyouUriageErrData(i + 1, strReadLine, dtHanyouUriageErr)
                        Continue For
                    End If
                    '�֑������`�F�b�N
                    If Not commonCheck.ChkKinjiMoji(strReadLine) Then
                        '�G���[�f�[�^���쐬����
                        Call SetHanyouUriageErrData(i + 1, strReadLine, dtHanyouUriageErr)
                        Continue For
                    End If
                    '���l�^���ڃ`�F�b�N
                    If Not commonCheck.ChkSuuti(strReadLine, CsvInputCheck.URIAGE) Then
                        '�G���[�f�[�^���쐬����
                        Call SetHanyouUriageErrData(i + 1, strReadLine, dtHanyouUriageErr)
                        Continue For
                    End If
                    '==========��2015/11/19 chel1 �ǉ���======================
                    '���p�p�����`�F�b�N
                    If Not commonCheck.ChkHankakuEisuuji(strReadLine, CsvInputCheck.URIAGE) Then
                        '�G���[�f�[�^���쐬����
                        Call SetHanyouUriageErrData(i + 1, strReadLine, dtHanyouUriageErr)
                        Continue For
                    End If
                    '==========��2015/11/19 chel1 �ǉ���======================
                    '���݃`�F�b�N
                    If Not ChkMstSonZai(strReadLine) Then
                        '�G���[�f�[�^���쐬����
                        Call SetHanyouUriageErrData(i + 1, strReadLine, dtHanyouUriageErr)
                        Continue For
                    End If
                    '�K�{�`�F�b�N
                    If Not commonCheck.ChkNotNull(strReadLine, CsvInputCheck.URIAGE) Then
                        '�G���[�f�[�^���쐬����
                        Call SetHanyouUriageErrData(i + 1, strReadLine, dtHanyouUriageErr)
                        Continue For
                    End If

                    '���t�`�F�b�N
                    If Not commonCheck.ChkNotDate(strReadLine, CsvInputCheck.URIAGE) Then
                        '�G���[�f�[�^���쐬����
                        Call SetHanyouUriageErrData(i + 1, strReadLine, dtHanyouUriageErr)
                        Continue For
                    End If


                    '�s�ǉ�
                    Call SetHanyouUriageDataRow(strReadLine, dtHanyouUriageOk)
                End If
            Next
            '�G���[�L���t���O��ݒ肷��
            If dtHanyouUriageErr.Rows.Count > 0 Then
                strUmuFlg = "1"
            End If
            'CSV�t�@�C�����捞
            If Not CsvFileUpload(dtHanyouUriageOk, dtHanyouUriageErr, strUploadDate, strUserId, strNyuuryokuFileMei, strEdiJouhouSakuseiDate) Then
                Return Messages.Instance.MSG050E
            End If
        Catch ex As Exception
            Return Messages.Instance.MSG049E
        End Try

        Return String.Empty
    End Function
    ''' <summary>���݃`�F�b�N</summary>
    Private Function ChkMstSonZai(ByRef intFieldCount As String) As Boolean

        '==========��2015/11/19 ������ add����X�敪,����X����  �C����======================
        ''�����X(�����X�R�[�h)
        'If intFieldCount.Split(",")(7).Trim <> "" Then
        '    If Not hanyouUriageDA.SelSeikyuuSakiInput(intFieldCount.Split(",")(6).Trim, intFieldCount.Split(",")(7).Trim, intFieldCount.Split(",")(8).Trim) Then
        '        Return False
        '    End If
        'End If

        ''���i�R�[�h
        'Dim dtReturn As DataTable = hanyouUriageDA.SelSyouhinCdInput(intFieldCount.Split(",")(9).Trim)
        'If dtReturn.Rows.Count = 0 Then
        '    Return False
        'Else
        '    If intFieldCount.Split(",")(10).Trim = "" Then
        '        Dim arrLine() As String = Split(intFieldCount, ",")
        '        arrLine(10) = dtReturn.Rows(0).Item(0).ToString
        '        intFieldCount = String.Join(",", arrLine)
        '    End If
        'End If
        '�����X(�����X�R�[�h)
        If intFieldCount.Split(",")(9).Trim <> "" Then
            If Not hanyouUriageDA.SelSeikyuuSakiInput(intFieldCount.Split(",")(8).Trim, intFieldCount.Split(",")(9).Trim, intFieldCount.Split(",")(10).Trim) Then
                Return False
            End If
        End If

        '���i�R�[�h
        Dim dtReturn As DataTable = hanyouUriageDA.SelSyouhinCdInput(intFieldCount.Split(",")(11).Trim)
        If dtReturn.Rows.Count = 0 Then
            Return False
        Else
            If intFieldCount.Split(",")(12).Trim = "" Then
                Dim arrLine() As String = Split(intFieldCount, ",")
                arrLine(12) = dtReturn.Rows(0).Item(0).ToString
                intFieldCount = String.Join(",", arrLine)
            End If
        End If
        '==========��2015/11/19 ������ add����X�敪,����X����  �C����======================

        Return True
    End Function
    ''' <summary>�ėp����e�[�u�����쐬����</summary>
    ''' <param name="dtHanyouUriageOk">�ėp����e�[�u��</param>
    Public Sub SetHanyouUriageOk(ByRef dtHanyouUriageOk As Data.DataTable)
        dtHanyouUriageOk.Columns.Add("torikesi")                '���
        dtHanyouUriageOk.Columns.Add("tekiyou")                 '�E�v
        dtHanyouUriageOk.Columns.Add("uri_date")                '����N����
        dtHanyouUriageOk.Columns.Add("denpyou_uri_date")        '�`�[����N����
        dtHanyouUriageOk.Columns.Add("seikyuu_date")            '�����N����
        '==========��2015/11/19 ������ add����X�敪,����X����  �C����======================
        dtHanyouUriageOk.Columns.Add("uriage_ten_kbn")          '����X�敪
        dtHanyouUriageOk.Columns.Add("uriage_ten_cd")           '����X����
        '==========��2015/11/19 ������ add����X�敪,����X����  �C����======================
        dtHanyouUriageOk.Columns.Add("seikyuu_saki_cd")         '������R�[�h	
        dtHanyouUriageOk.Columns.Add("seikyuu_saki_brc")        '������}��	
        dtHanyouUriageOk.Columns.Add("seikyuu_saki_kbn")        '������敪
        dtHanyouUriageOk.Columns.Add("syouhin_cd")              '���i�R�[�h
        dtHanyouUriageOk.Columns.Add("hin_mei")                 '�i��
        dtHanyouUriageOk.Columns.Add("suu")                     '����			
        dtHanyouUriageOk.Columns.Add("tanka")                   '�P��
        dtHanyouUriageOk.Columns.Add("syanai_genka")            '�Г�����
        dtHanyouUriageOk.Columns.Add("zei_kbn")                 '�ŋ敪			
        dtHanyouUriageOk.Columns.Add("syouhizei_gaku")          '����Ŋz	
        dtHanyouUriageOk.Columns.Add("uri_keijyou_flg")         '���㏈��FLG(����v��FLG)	
        dtHanyouUriageOk.Columns.Add("uri_keijyou_date")        '���㏈����(����v���)	
        dtHanyouUriageOk.Columns.Add("kbn")                     '�敪
        dtHanyouUriageOk.Columns.Add("bangou")                  '�ԍ�
        dtHanyouUriageOk.Columns.Add("sesyu_mei")               '�{�喼
    End Sub

    ''' <summary>�ėp����G���[�e�[�u�����쐬����</summary>
    ''' <param name="dtHanyouUriageErr">�ėp����G���[�e�[�u��</param>
    Public Sub SetHanyouUriageErr(ByRef dtHanyouUriageErr As Data.DataTable)
        dtHanyouUriageErr.Columns.Add("edi_jouhou_sakusei_date")        'EDI���쐬��
        dtHanyouUriageErr.Columns.Add("torikesi")                       '���
        dtHanyouUriageErr.Columns.Add("tekiyou")                        '�E�v
        dtHanyouUriageErr.Columns.Add("uri_date")                       '����N����
        dtHanyouUriageErr.Columns.Add("denpyou_uri_date")               '�`�[����N����
        dtHanyouUriageErr.Columns.Add("seikyuu_date")                   '�����N����
        '==========��2015/11/19 ������ add����X�敪,����X����  �C����======================
        dtHanyouUriageErr.Columns.Add("uriage_ten_kbn")          '����X�敪
        dtHanyouUriageErr.Columns.Add("uriage_ten_cd")           '����X����
        '==========��2015/11/19 ������ add����X�敪,����X����  �C����======================
        dtHanyouUriageErr.Columns.Add("seikyuu_saki_cd")                '������R�[�h	
        dtHanyouUriageErr.Columns.Add("seikyuu_saki_brc")               '������}��	
        dtHanyouUriageErr.Columns.Add("seikyuu_saki_kbn")               '������敪
        dtHanyouUriageErr.Columns.Add("syouhin_cd")                     '���i�R�[�h
        dtHanyouUriageErr.Columns.Add("hin_mei")                        '�i��
        dtHanyouUriageErr.Columns.Add("suu")                            '����			
        dtHanyouUriageErr.Columns.Add("tanka")                          '�P��
        dtHanyouUriageErr.Columns.Add("syanai_genka")                   '�Г�����
        dtHanyouUriageErr.Columns.Add("zei_kbn")                        '�ŋ敪			
        dtHanyouUriageErr.Columns.Add("syouhizei_gaku")                 '����Ŋz	
        dtHanyouUriageErr.Columns.Add("uri_keijyou_flg")                '���㏈��FLG(����v��FLG)	
        dtHanyouUriageErr.Columns.Add("uri_keijyou_date")               '���㏈����(����v���)	
        dtHanyouUriageErr.Columns.Add("kbn")                            '�敪
        dtHanyouUriageErr.Columns.Add("bangou")                         '�ԍ�
        dtHanyouUriageErr.Columns.Add("sesyu_mei")                      '�{�喼
        dtHanyouUriageErr.Columns.Add("gyou_no")                        '�sNO
    End Sub

    ''' <summary>�ėp����G���[�f�[�^���쐬����</summary>
    ''' <param name="intLineNo">CSV�t�@�C���̊Y���s�̍sNO</param>
    ''' <param name="strLine">CSV�t�@�C���̊Y���s�̃f�[�^</param>
    ''' <param name="dtHanyouUriageError">�ėp����G���[�f�[�^</param>
    Private Sub SetHanyouUriageErrData(ByVal intLineNo As Integer, _
                                       ByVal strLine As String, _
                                       ByRef dtHanyouUriageError As Data.DataTable)
        Dim intMaxCount As Integer
        Dim commonCheck As New CsvInputCheck
        Dim dr As Data.DataRow
        dr = dtHanyouUriageError.NewRow

        If strLine.Split(",").Length < CsvInputCheck.URIAGE_FIELD_COUNT Then
            intMaxCount = strLine.Split(",").Length
        Else
            intMaxCount = CsvInputCheck.URIAGE_FIELD_COUNT
        End If

        For i As Integer = 0 To intMaxCount - 1
            '�ő咷��؂���
            dr.Item(i) = commonCheck.CutMaxLength(strLine.Split(",")(i).Trim, commonCheck.URIAGE_MAX_LENGTH(i))
        Next
        '�s��
        dr.Item(CsvInputCheck.URIAGE_FIELD_COUNT) = CStr(intLineNo)

        dtHanyouUriageError.Rows.Add(dr)

    End Sub

    ''' <summary>�ėp����f�[�^���쐬����</summary>
    ''' <param name="strLine">CSV�t�@�C���̊Y���s�̃f�[�^</param>
    ''' <param name="dtHanyouUriageOk">�ėp����f�[�^</param>
    Public Sub SetHanyouUriageDataRow(ByVal strLine As String, _
                                      ByRef dtHanyouUriageOk As Data.DataTable)
        Dim dr As Data.DataRow
        dr = dtHanyouUriageOk.NewRow
        dr.Item("torikesi") = strLine.Split(",")(1).Trim                '���
        dr.Item("tekiyou") = strLine.Split(",")(2).Trim                     '�E�v
        dr.Item("uri_date") = strLine.Split(",")(3).Trim                    '����N����
        dr.Item("denpyou_uri_date") = strLine.Split(",")(4).Trim            '�`�[����N����
        dr.Item("seikyuu_date") = strLine.Split(",")(5).Trim                '�����N����
        '==========��2015/11/19 ������ add����X�敪,����X����  �C����======================
        'dr.Item("seikyuu_saki_cd") = strLine.Split(",")(6).Trim             '������R�[�h
        'dr.Item("seikyuu_saki_brc") = strLine.Split(",")(7).Trim            '������}��
        'dr.Item("seikyuu_saki_kbn") = strLine.Split(",")(8).Trim            '������敪
        'dr.Item("syouhin_cd") = strLine.Split(",")(9).Trim                  '���i�R�[�h
        'dr.Item("hin_mei") = strLine.Split(",")(10).Trim                    '���i�R�[�h
        'dr.Item("suu") = strLine.Split(",")(11).Trim                        '����
        'dr.Item("tanka") = strLine.Split(",")(12).Trim                      '�P��
        'dr.Item("syanai_genka") = strLine.Split(",")(13).Trim               '�Г�����
        'dr.Item("zei_kbn") = strLine.Split(",")(14).Trim                    '�ŋ敪
        'dr.Item("syouhizei_gaku") = strLine.Split(",")(15).Trim             '����Ŋz
        'dr.Item("uri_keijyou_flg") = strLine.Split(",")(16).Trim            '���㏈��FLG
        'dr.Item("uri_keijyou_date") = strLine.Split(",")(17).Trim           '���㏈����
        'dr.Item("kbn") = strLine.Split(",")(18).Trim                        '�敪
        'dr.Item("bangou") = strLine.Split(",")(19).Trim                     '�ԍ�
        'dr.Item("sesyu_mei") = strLine.Split(",")(20).Trim                  '�{�喼
        dr.Item("uriage_ten_kbn") = strLine.Split(",")(6).Trim              '����X�敪
        dr.Item("uriage_ten_cd") = strLine.Split(",")(7).Trim               '����X����
        dr.Item("seikyuu_saki_cd") = strLine.Split(",")(8).Trim             '������R�[�h
        dr.Item("seikyuu_saki_brc") = strLine.Split(",")(9).Trim            '������}��
        dr.Item("seikyuu_saki_kbn") = strLine.Split(",")(10).Trim           '������敪
        dr.Item("syouhin_cd") = strLine.Split(",")(11).Trim                 '���i�R�[�h
        dr.Item("hin_mei") = strLine.Split(",")(12).Trim                    '���i�R�[�h
        dr.Item("suu") = strLine.Split(",")(13).Trim                        '����
        dr.Item("tanka") = strLine.Split(",")(14).Trim                      '�P��
        dr.Item("syanai_genka") = strLine.Split(",")(15).Trim               '�Г�����
        dr.Item("zei_kbn") = strLine.Split(",")(16).Trim                    '�ŋ敪
        dr.Item("syouhizei_gaku") = strLine.Split(",")(17).Trim             '����Ŋz
        dr.Item("uri_keijyou_flg") = strLine.Split(",")(18).Trim            '���㏈��FLG
        dr.Item("uri_keijyou_date") = strLine.Split(",")(19).Trim           '���㏈����
        dr.Item("kbn") = strLine.Split(",")(20).Trim                        '�敪
        dr.Item("bangou") = strLine.Split(",")(21).Trim                     '�ԍ�
        dr.Item("sesyu_mei") = strLine.Split(",")(22).Trim                  '�{�喼
        '==========��2015/11/19 ������ add����X�敪,����X����  �C����======================

        dtHanyouUriageOk.Rows.Add(dr)
    End Sub

    ''' <summary>CSV�t�@�C�����捞����</summary>
    ''' <param name="dtHanyouUriageOk">�ėp����f�[�^</param>
    ''' <param name="dtHanyouUriageErr">�ėp����G���[�f�[�^</param>
    ''' <param name="strUserId">���[�U�[ID</param>
    ''' <param name="strNyuuryokuFileMei">���̓t�@�C����</param>
    ''' <param name="strEdiJouhouSakuseiDate">EDI���쐬��</param>
    ''' <returns>�����E���s�敪</returns>
    Public Function CsvFileUpload(ByVal dtHanyouUriageOk As Data.DataTable, _
                                  ByVal dtHanyouUriageErr As Data.DataTable, _
                                  ByVal strUploadDate As String, _
                                  ByVal strUserId As String, _
                                  ByVal strNyuuryokuFileMei As String, _
                                  ByVal strEdiJouhouSakuseiDate As String) As Boolean

        Using scope As New TransactionScope(TransactionScopeOption.RequiresNew)
            Try
                '�ėp����}�X�^��o�^����
                If dtHanyouUriageOk.Rows.Count > 0 Then
                    If Not hanyouUriageDA.InsHanyouUriage(dtHanyouUriageOk, strUserId) Then
                        Throw New ApplicationException
                    End If
                End If
                '�ėp����G���[���e�[�u����o�^����
                If dtHanyouUriageErr.Rows.Count > 0 Then
                    If Not hanyouUriageDA.InsHanyouUriageErr(dtHanyouUriageErr, strUploadDate, strUserId) Then
                        Throw New ApplicationException
                    End If
                End If
                '�A�b�v���[�h�Ǘ��e�[�u����o�^����
                If Not hanyouUriageDA.InsUploadKanri(strUploadDate, strNyuuryokuFileMei, strEdiJouhouSakuseiDate, CInt(IIf(dtHanyouUriageErr.Rows.Count, 1, 0)), strUserId) Then
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
