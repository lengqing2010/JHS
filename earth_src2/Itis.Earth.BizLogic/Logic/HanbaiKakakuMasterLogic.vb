Imports System.Transactions
Imports Itis.Earth.DataAccess
Public Class HanbaiKakakuMasterLogic
    ''' <summary>�̔����i�}�X�^�N���X�̃C���X�^���X���� </summary>
    Private HanbaiKakakuMasterDA As New HanbaiKakakuMasterDataAccess
    ''' <summary>CSV�捞���ʃN���X�̃C���X�^���X���� </summary>
    Private commonCheck As New CsvInputCheck

    ''' <summary>������ʂ��擾����</summary>
    ''' <returns>������ʃf�[�^�e�[�u��</returns>
    Public Function GetAiteSakiSyubetu() As Data.DataTable
        Return HanbaiKakakuMasterDA.SelAiteSakiSyubetu()
    End Function
    ''' <summary>���i���擾����</summary>
    ''' <returns>���i�f�[�^�e�[�u��</returns>
    Public Function GetSyouhin() As Data.DataTable
        Return HanbaiKakakuMasterDA.SelSyouhin()
    End Function
    ''' <summary>�������@���擾����</summary>
    ''' <returns>�������@�f�[�^�e�[�u��</returns>
    Public Function GetTyousaHouhou() As Data.DataTable
        Return HanbaiKakakuMasterDA.SelTyousaHouhou()
    End Function
    '''<summary>���������擾����</summary>
    ''' <param name="strAitesakiSyubetu">�������</param>
    ''' <param name="strTorikesiAitesaki">��������敪</param>
    ''' <param name="strAitesakiCd">�����R�[�h</param>
    ''' <returns>�������f�[�^�e�[�u��</returns>
    Public Function GetAiteSaki(ByVal strAitesakiSyubetu As String, _
                                ByVal strTorikesiAitesaki As String, _
                                ByVal strAitesakiCd As String) As Data.DataTable
        Return HanbaiKakakuMasterDA.SelAiteSaki(strAitesakiSyubetu, strTorikesiAitesaki, strAitesakiCd)
    End Function
    ''' <summary>�̔����i�f�[�^���擾����</summary>
    ''' <param name="dtHanbaiKakakuInfo">�p�����[�^</param>
    ''' <returns>�̔����i�f�[�^�e�[�u��</returns>
    Public Function GetHanbaiKakakuInfo(ByVal dtHanbaiKakakuInfo As HanbaiKakakuMasterDataSet.Param_HanbaiKakakuInfoDataTable) _
            As HanbaiKakakuMasterDataSet.HanbaiKakakuInfoTableDataTable
        Return HanbaiKakakuMasterDA.SelHanbaiKakakuInfo(dtHanbaiKakakuInfo)
    End Function
    ''' <summary>�̔����i�u�n��E�c�Ə��E�w��Ȃ��`�F�b�N�{�b�N�X�v=�`�F�b�N�̏ꍇ�̃f�[�^���擾����</summary>
    ''' <param name="dtHanbaiKakakuInfo">�p�����[�^</param>
    ''' <returns>�̔����i�f�[�^�e�[�u��</returns>
    Public Function GetHanbaiKakakuSeiteNasiInfo(ByVal dtHanbaiKakakuInfo As HanbaiKakakuMasterDataSet.Param_HanbaiKakakuInfoDataTable) _
            As HanbaiKakakuMasterDataSet.HanbaiKakakuInfoTableDataTable
        Return HanbaiKakakuMasterDA.SelHanbaiKakakuSeiteNasiInfo(dtHanbaiKakakuInfo)
    End Function
    ''' <summary>�̔����i�f�[�^�������擾����</summary>
    ''' <param name="dtHanbaiKakakuInfo">�p�����[�^</param>
    ''' <returns>�̔����i�f�[�^����</returns>
    Public Function GetHanbaiKakakuInfoCount(ByVal dtHanbaiKakakuInfo As HanbaiKakakuMasterDataSet.Param_HanbaiKakakuInfoDataTable) _
            As Integer
        Return HanbaiKakakuMasterDA.SelHanbaiKakakuInfoCount(dtHanbaiKakakuInfo)
    End Function
    ''' <summary>�̔����i�f�[�^�u�n��E�c�Ə��E�w��Ȃ��`�F�b�N�{�b�N�X�v=�`�F�b�N�̏ꍇ�̌������擾����</summary>
    ''' <param name="dtHanbaiKakakuInfo">�p�����[�^</param>
    ''' <returns>�̔����i�f�[�^����</returns>
    Public Function GetHanbaiKakakuSeiteNasiInfoCount(ByVal dtHanbaiKakakuInfo As HanbaiKakakuMasterDataSet.Param_HanbaiKakakuInfoDataTable) _
            As Integer
        Return HanbaiKakakuMasterDA.SelHanbaiKakakuSeiteNasiinfoCount(dtHanbaiKakakuInfo)
    End Function
    ''' <summary>���ݒ���܂ޔ̔����iCSV�f�[�^�������擾����</summary>
    ''' <param name="dtHanbaiKakakuInfo">�p�����[�^</param>
    ''' <returns>���ݒ���܂ޔ̔����iCSV�f�[�^����</returns>
    Public Function GetMiSeteiHanbaiKakakuCSVCount(ByVal dtHanbaiKakakuInfo As HanbaiKakakuMasterDataSet.Param_HanbaiKakakuInfoDataTable) _
            As Long
        Return HanbaiKakakuMasterDA.SelMiSeteiHanbaiKakakuCSVCount(dtHanbaiKakakuInfo)
    End Function
    ''' <summary>���ݒ���܂ޔ̔����iCSV�f�[�^���擾����</summary>
    ''' <param name="dtHanbaiKakakuInfo">�p�����[�^</param>
    ''' <returns>���ݒ���܂ޔ̔����iCSV�f�[�^�e�[�u��</returns>
    Public Function GetMiSeteiHanbaiKakakuCSVInfo(ByVal dtHanbaiKakakuInfo As HanbaiKakakuMasterDataSet.Param_HanbaiKakakuInfoDataTable) _
            As HanbaiKakakuMasterDataSet.HanbaiKakakuCSVInfoTableDataTable
        Return HanbaiKakakuMasterDA.SelMiSeteiHanbaiKakakuCSVInfo(dtHanbaiKakakuInfo)
    End Function
    ''' <summary>�̔����iCSV�f�[�^���擾����</summary>
    ''' <param name="dtHanbaiKakakuInfo">�p�����[�^</param>
    ''' <returns>�̔����iCSV�f�[�^�e�[�u��</returns>
    Public Function GetHanbaiKakakuCSVInfo(ByVal dtHanbaiKakakuInfo As HanbaiKakakuMasterDataSet.Param_HanbaiKakakuInfoDataTable) _
            As HanbaiKakakuMasterDataSet.HanbaiKakakuCSVInfoTableDataTable
        Return HanbaiKakakuMasterDA.SelHanbaiKakakuCSVInfo(dtHanbaiKakakuInfo)
    End Function
    ''' <summary>�A�b�v���[�h�Ǘ����擾����</summary>
    ''' <returns>�A�b�v���[�h�Ǘ��f�[�^�e�[�u��</returns>
    Public Function GetUploadKanri() As Data.DataTable
        Return HanbaiKakakuMasterDA.SelUploadKanri()
    End Function
    ''' <summary>�A�b�v���[�h�Ǘ��̌������擾����</summary>
    ''' <returns>�A�b�v���[�h�Ǘ��̌���</returns>
    Public Function GetUploadKanriCount() As Integer
        Return HanbaiKakakuMasterDA.SelUploadKanriCount()
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
        Dim dtHanbaiKakakuOk As New Data.DataTable              '�̔����i�}�X�^
        Dim dtHanbaiKakakuError As New Data.DataTable           '�̔����i�G���[
        Dim strUploadDate As String                             '�A�b�v���[�h����
        Dim ninsyou As New Ninsyou()
        Dim strUserId As String                                 '���[�U�[ID
        Dim strMaxUpload As String                              'CSV�捞�̏������

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
        '�̔����i�}�X�^���쐬����
        Call SetHanbaiKakakuOk(dtHanbaiKakakuOk)
        '�̔����i�G���[�e�[�u�����쐬����
        Call SetHanbaiKakakuError(dtHanbaiKakakuError)

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
            strEdiJouhouSakuseiDate = Right("0" & strCsvLine(1).Split(",")(0), 10)

            'CSV�t�@�C�����`�F�b�N
            For i As Integer = 1 To strCsvLine.Length - 1
                strReadLine = strCsvLine(i)
                If (Not strReadLine Is Nothing) AndAlso (Replace(strReadLine, ",", "") <> "") Then
                    'EDI���쐬�����O0���ߕϊ�
                    If strReadLine.Split(",")(0).Length < 10 Then
                        strReadLine = "0" & strReadLine
                    End If
                    '�������ߕϊ�
                    If strReadLine.Split(",").Length < CsvInputCheck.HANBAI_FIELD_COUNT Then
                        strReadLine = strReadLine & StrDup(CsvInputCheck.HANBAI_FIELD_COUNT - strReadLine.Split(",").Length, ",")
                    End If
                    '�t�B�[���h���`�F�b�N
                    If Not commonCheck.ChkFieldCount(strReadLine.Split(",").Length, CsvInputCheck.HANBAI) Then
                        '�G���[�f�[�^���쐬����
                        Call SetHanbaiKakakuErrData(i + 1, strReadLine, dtHanbaiKakakuError)
                        Continue For
                    End If
                    '���ڍő咷�`�F�b�N
                    If Not commonCheck.ChkMaxLength(strReadLine, CsvInputCheck.HANBAI) Then
                        '�G���[�f�[�^���쐬����
                        Call SetHanbaiKakakuErrData(i + 1, strReadLine, dtHanbaiKakakuError)
                        Continue For
                    End If
                    '�֑������`�F�b�N
                    If Not commonCheck.ChkKinjiMoji(strReadLine) Then
                        '�G���[�f�[�^���쐬����
                        Call SetHanbaiKakakuErrData(i + 1, strReadLine, dtHanbaiKakakuError)
                        Continue For
                    End If
                    '���l�^���ڃ`�F�b�N
                    If Not commonCheck.ChkSuuti(strReadLine, CsvInputCheck.HANBAI) Then
                        '�G���[�f�[�^���쐬����
                        Call SetHanbaiKakakuErrData(i + 1, strReadLine, dtHanbaiKakakuError)
                        Continue For
                    End If
                    '�K�{�`�F�b�N
                    If Not commonCheck.ChkNotNull(strReadLine, CsvInputCheck.HANBAI) Then
                        '�G���[�f�[�^���쐬����
                        Call SetHanbaiKakakuErrData(i + 1, strReadLine, dtHanbaiKakakuError)
                        Continue For
                    End If
                    '�����R�[�h���O0���ߕϊ�
                    SetAitesakiCd(strReadLine)
                    '���݃`�F�b�N
                    If Not ChkMstSonZai(strReadLine) Then
                        '�G���[�f�[�^���쐬����
                        Call SetHanbaiKakakuErrData(i + 1, strReadLine, dtHanbaiKakakuError)
                        Continue For
                    End If
                    '�X�V�����`�F�b�N
                    If Not commonCheck.ChkKousinJyouken(strReadLine, CsvInputCheck.HANBAI) Then
                        Continue For
                    End If
                    '�X�V��ǉ��𔻒f����
                    Call SetKousinKbn(strReadLine, dtHanbaiKakakuOk)
                End If
            Next
            '�G���[�L���t���O��ݒ肷��
            If dtHanbaiKakakuError.Rows.Count > 0 Then
                strUmuFlg = "1"
            End If
            'CSV�t�@�C�����捞
            If Not CsvFileUpload(dtHanbaiKakakuOk, dtHanbaiKakakuError, strUploadDate, strUserId, strNyuuryokuFileMei, strEdiJouhouSakuseiDate) Then
                Return Messages.Instance.MSG050E
            End If
        Catch ex As Exception
            Return Messages.Instance.MSG049E
        End Try

        Return String.Empty
    End Function
    ''' <summary>�̔����i�e�[�u�����쐬����</summary>
    ''' <param name="dtHanbaiKakakuOk">�̔����i�e�[�u��</param>
    Public Sub SetHanbaiKakakuOk(ByRef dtHanbaiKakakuOk As Data.DataTable)

        Dim dc As Data.DataColumn

        dc = New Data.DataColumn
        dc.ColumnName = "key"               '�L�[(������� + �����R�[�h + ���i�R�[�h + �������@NO)
        dtHanbaiKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "ins_upd_flg"       '�ǉ��X�VFLG(0:�ǉ�; 1:�X�V)
        dtHanbaiKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "aitesaki_syubetu"  '�������
        dtHanbaiKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "aitesaki_cd"       '�����R�[�h
        dtHanbaiKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "syouhin_cd"        '���i�R�[�h
        dtHanbaiKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tys_houhou_no"     '�������@NO
        dtHanbaiKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "torikesi"          '���
        dtHanbaiKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "koumuten_seikyuu_gaku"  '�H���X�������z
        dtHanbaiKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "koumuten_seikyuu_gaku_henkou_flg"   '�H���X�������z�ύXFLG
        dtHanbaiKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "jitu_seikyuu_gaku"  '���������z
        dtHanbaiKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "jitu_seikyuu_gaku_henkou_flg"   '���������z�ύXFLG
        dtHanbaiKakakuOk.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "koukai_flg"        '���J�t���O
        dtHanbaiKakakuOk.Columns.Add(dc)

    End Sub
    ''' <summary>�̔����i�G���[�e�[�u�����쐬����</summary>
    ''' <param name="dtHanbaiKakakuError">�̔����i�G���[�e�[�u��</param>
    Public Sub SetHanbaiKakakuError(ByRef dtHanbaiKakakuError As Data.DataTable)

        Dim dc As Data.DataColumn
        dc = New Data.DataColumn
        dc.ColumnName = "edi_jouhou_sakusei_date"           'EDI���쐬��
        dtHanbaiKakakuError.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "aitesaki_syubetu"                  '�������
        dtHanbaiKakakuError.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "aitesaki_cd"                       '�����R�[�h
        dtHanbaiKakakuError.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "aitesaki_mei"                      '����於
        dtHanbaiKakakuError.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "syouhin_cd"                        '���i�R�[�h
        dtHanbaiKakakuError.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "syouhin_mei"                       '���i��
        dtHanbaiKakakuError.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tys_houhou_no"                     '�������@NO
        dtHanbaiKakakuError.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "tys_houhou"                        '�������@
        dtHanbaiKakakuError.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "torikesi"                          '���
        dtHanbaiKakakuError.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "koumuten_seikyuu_gaku"             '�H���X�������z
        dtHanbaiKakakuError.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "koumuten_seikyuu_gaku_henkou_flg"  '�H���X�������z�ύXFLG
        dtHanbaiKakakuError.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "jitu_seikyuu_gaku"                  '���������z
        dtHanbaiKakakuError.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "jitu_seikyuu_gaku_henkou_flg"      '���������z�ύXFLG
        dtHanbaiKakakuError.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "koukai_flg"                        '���J�t���O
        dtHanbaiKakakuError.Columns.Add(dc)
        dc = New Data.DataColumn
        dc.ColumnName = "gyou_no"                           '�sNO
        dtHanbaiKakakuError.Columns.Add(dc)

    End Sub
    ''' <summary>�̔����i�G���[�f�[�^���쐬����</summary>
    ''' <param name="intLineNo">CSV�t�@�C���̊Y���s�̍sNO</param>
    ''' <param name="strLine">CSV�t�@�C���̊Y���s�̃f�[�^</param>
    ''' <param name="dtHanbaiKakakuError">�̔����i�G���[�f�[�^</param>
    Private Sub SetHanbaiKakakuErrData(ByVal intLineNo As Integer, _
                                       ByVal strLine As String, _
                                       ByRef dtHanbaiKakakuError As Data.DataTable)

        Dim intMaxCount As Integer

        Dim dr As Data.DataRow
        dr = dtHanbaiKakakuError.NewRow

        If strLine.Split(",").Length < CsvInputCheck.HANBAI_FIELD_COUNT Then
            intMaxCount = strLine.Split(",").Length
        Else
            intMaxCount = CsvInputCheck.HANBAI_FIELD_COUNT
        End If

        For i As Integer = 0 To intMaxCount - 1
            '�ő咷��؂���
            dr.Item(i) = commonCheck.CutMaxLength(strLine.Split(",")(i).Trim, commonCheck.HANBAI_MAX_LENGTH(i))
        Next
        '�s��
        dr.Item(CsvInputCheck.HANBAI_FIELD_COUNT) = CStr(intLineNo)

        dtHanbaiKakakuError.Rows.Add(dr)

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
        strLine = String.Join(",", arrLine)

    End Sub
    ''' <summary>���݃`�F�b�N</summary>
    ''' <param name="strLine">CSV�t�@�C���̊Y���s�̃f�[�^</param>
    Private Function ChkMstSonZai(ByVal strLine As String) As Boolean

        '�����(��ʁE�R�[�h)
        If Not HanbaiKakakuMasterDA.SelAiteSaki(strLine.Split(",")(1).Trim, strLine.Split(",")(2).Trim) Then
            Return False
        End If

        '���i�R�[�h
        If Not HanbaiKakakuMasterDA.SelSyouhinCd(strLine.Split(",")(4).Trim) Then
            Return False
        End If

        '�������@NO
        If Not HanbaiKakakuMasterDA.SelTysHouhou(strLine.Split(",")(6).Trim) Then
            Return False
        End If

        Return True
    End Function
    ''' <summary>�X�V��ǉ��𔻒f����</summary>
    ''' <param name="strLine">CSV�t�@�C���̊Y���s�̃f�[�^</param>
    ''' <param name="dtHanbaiKakakuOk">�̔����i�f�[�^</param> 
    Private Sub SetKousinKbn(ByVal strLine As String, ByRef dtHanbaiKakakuOk As Data.DataTable)

        '�̔����i�}�X�^���݃`�F�b�N
        If HanbaiKakakuMasterDA.SelHanbaiKakaku(strLine.Split(",")(1).Trim, strLine.Split(",")(2).Trim, strLine.Split(",")(4).Trim, strLine.Split(",")(6).Trim) Then
            '�̔����i�}�X�^�����݂���ꍇ�A�X�V�敪��ݒ肷��
            Call SetHanbaiKakakuDataRow(strLine, dtHanbaiKakakuOk, "1")
        Else
            'CSV�t�@�C���ɍs�O�̃f�[�^������ꍇ
            If dtHanbaiKakakuOk.Rows.Count > 0 Then
                '���݃t���O
                Dim blnSonzaiFlg As Boolean = False

                '�s�O�f�[�^�Ɏ�L�[�����邩�ǂ����𔻒f����
                For i As Integer = 0 To dtHanbaiKakakuOk.Rows.Count - 1
                    If dtHanbaiKakakuOk.Rows(i).Item("key").ToString.Trim.Equals(GetHanbaiKakakuKey(strLine)) Then
                        blnSonzaiFlg = True
                        Exit For
                    End If
                Next

                If blnSonzaiFlg Then
                    '�s�O�f�[�^�Ɏ�L�[������ꍇ�A�X�V�敪��ݒ肷��
                    Call SetHanbaiKakakuDataRow(strLine, dtHanbaiKakakuOk, "1")
                Else
                    '�s�O�f�[�^�Ɏ�L�[���Ȃ��ꍇ�A�ǉ��敪��ݒ肷��
                    Call SetHanbaiKakakuDataRow(strLine, dtHanbaiKakakuOk, "0")
                End If
            Else
                '�̔����i�}�X�^�����݂��Ȃ��ACSV�t�@�C���ɍs�O�̃f�[�^���Ȃ��ꍇ�A�ǉ��敪��ݒ肷��
                Call SetHanbaiKakakuDataRow(strLine, dtHanbaiKakakuOk, "0")
            End If

        End If

    End Sub
    ''' <summary>�̔����i�f�[�^��key��ݒ肷��</summary>
    ''' <param name="strLine">CSV�t�@�C���̊Y���s�̃f�[�^</param>
    ''' <returns>�Y���s�̃f�[�^�̎�L�[</returns>
    Public Function GetHanbaiKakakuKey(ByVal strLine As String) As String

        Return strLine.Split(",")(1).Trim & "," & strLine.Split(",")(2).Trim & "," & strLine.Split(",")(4).Trim & "," & strLine.Split(",")(6).Trim

    End Function
    ''' <summary>�̔����i�f�[�^���쐬����</summary>
    ''' <param name="strLine">CSV�t�@�C���̊Y���s�̃f�[�^</param>
    ''' <param name="dtHanbaiKakakuOk">�̔����i�f�[�^</param>
    ''' <param name="strInsUpdFlg">�X�V�E�ǉ��敪</param>
    Public Sub SetHanbaiKakakuDataRow(ByVal strLine As String, _
                                      ByRef dtHanbaiKakakuOk As Data.DataTable, _
                                      ByVal strInsUpdFlg As String)

        Dim dr As Data.DataRow
        dr = dtHanbaiKakakuOk.NewRow
        dr.Item("key") = GetHanbaiKakakuKey(strLine)                '��L�[
        dr.Item("ins_upd_flg") = strInsUpdFlg                       '�X�V�E�ǉ��敪
        dr.Item("aitesaki_syubetu") = strLine.Split(",")(1).Trim    '�������
        dr.Item("aitesaki_cd") = strLine.Split(",")(2).Trim         '�����R�[�h
        dr.Item("syouhin_cd") = strLine.Split(",")(4).Trim          '���i�R�[�h
        dr.Item("tys_houhou_no") = strLine.Split(",")(6).Trim       '�������@NO
        dr.Item("torikesi") = IIf(strLine.Split(",")(8).Trim.Equals(String.Empty), "0", strLine.Split(",")(8).Trim) '���
        dr.Item("koumuten_seikyuu_gaku") = strLine.Split(",")(9).Trim               '�H���X�������z
        dr.Item("koumuten_seikyuu_gaku_henkou_flg") = strLine.Split(",")(10).Trim   '�H���X�������z�ύXFLG
        dr.Item("jitu_seikyuu_gaku") = strLine.Split(",")(11).Trim                  '���������z
        dr.Item("jitu_seikyuu_gaku_henkou_flg") = strLine.Split(",")(12).Trim       '���������z�ύXFLG
        dr.Item("koukai_flg") = strLine.Split(",")(13).Trim                         '���J�t���O

        dtHanbaiKakakuOk.Rows.Add(dr)

    End Sub
    ''' <summary>CSV�t�@�C�����捞����</summary>
    ''' <param name="dtHanbaiKakakuOk">�̔����i�f�[�^</param>
    ''' <param name="dtHanbaiKakakuError">�̔����i�G���[�f�[�^</param>
    ''' <param name="strUserId">���[�U�[ID</param>
    ''' <param name="strNyuuryokuFileMei">���̓t�@�C����</param>
    ''' <param name="strEdiJouhouSakuseiDate">EDI���쐬��</param>
    ''' <returns>�����E���s�敪</returns>
    Public Function CsvFileUpload(ByVal dtHanbaiKakakuOk As Data.DataTable, _
                                  ByVal dtHanbaiKakakuError As Data.DataTable, _
                                  ByVal strUploadDate As String, _
                                  ByVal strUserId As String, _
                                  ByVal strNyuuryokuFileMei As String, _
                                  ByVal strEdiJouhouSakuseiDate As String) As Boolean

        Using scope As New TransactionScope(TransactionScopeOption.RequiresNew)
            Try
                '�̔����i�}�X�^��o�^����
                If dtHanbaiKakakuOk.Rows.Count > 0 Then
                    If Not HanbaiKakakuMasterDA.InsUpdHanbaiKakaku(dtHanbaiKakakuOk, strUserId) Then
                        Throw New ApplicationException
                    End If
                End If
                '�̔����i�G���[���e�[�u����o�^����
                If dtHanbaiKakakuError.Rows.Count > 0 Then
                    If Not HanbaiKakakuMasterDA.InsHanbaiKakakuError(dtHanbaiKakakuError, strUploadDate, strUserId) Then
                        Throw New ApplicationException
                    End If
                End If
                '�A�b�v���[�h�Ǘ��e�[�u����o�^����
                If Not HanbaiKakakuMasterDA.InsUploadKanri(strUploadDate, strNyuuryokuFileMei, strEdiJouhouSakuseiDate, CInt(IIf(dtHanbaiKakakuError.Rows.Count, 1, 0)), strUserId) Then
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
    ''' <summary>�̔����i�G���[�����擾����</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI���쐬��</param>
    ''' <param name="strSyoriDate">��������</param>
    ''' <returns>�̔����i�G���[�f�[�^�e�[�u��</returns>
    Public Function GetHanbaiKakakuErr(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoriDate As String) _
                As HanbaiKakakuMasterDataSet.HanbaiKakakuErrTableDataTable
        Return HanbaiKakakuMasterDA.SelHanbaiKakakuErr(strEdiJouhouSakuseiDate, strSyoriDate)
    End Function
    ''' <summary>�̔����i�G���[�������擾����</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI���쐬��</param>
    ''' <param name="strSyoriDate">��������</param>
    ''' <returns>�̔����i�G���[����</returns>
    Public Function GetHanbaiKakakuErrCount(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoriDate As String) As String
        Return HanbaiKakakuMasterDA.SelHanbaiKakakuErrCount(strEdiJouhouSakuseiDate, strSyoriDate)
    End Function
    ''' <summary>�̔����i�G���[CSV�����擾����</summary>
    ''' <param name="strEdiJouhouSakuseiDate">EDI���쐬��</param>
    ''' <param name="strSyoriDate">��������</param>
    ''' <returns>�̔����i�G���[CSV�f�[�^�e�[�u��</returns>
    Public Function SelHanbaiKakakuErrCsv(ByVal strEdiJouhouSakuseiDate As String, ByVal strSyoriDate As String) _
                    As HanbaiKakakuMasterDataSet.HanbaiKakakuErrCSVTableDataTable
        Return HanbaiKakakuMasterDA.SelHanbaiKakakuErrCsv(strEdiJouhouSakuseiDate, strSyoriDate)
    End Function

    '====================2011/05/16 �ԗ� �d�l�ύX �ǉ� �J�n��===========================
    ''' <summary>�̔����i�}�X�^�ʐݒ�f�[�^���擾����</summary>
    Public Function GetHanbaiKakakuKobeituSettei(ByVal strAiteSakiSyubetu As String, ByVal strAiteSakiCd As String, ByVal strSyouhinCd As String, ByVal strTyousaHouhouNo As String) As Data.DataTable

        Return HanbaiKakakuMasterDA.SelHanbaiKakakuKobeituSettei(strAiteSakiSyubetu, strAiteSakiCd, strSyouhinCd, strTyousaHouhouNo)

    End Function

    ''' <summary>�̔����i�}�X�^�ʐݒ�̑��݃`�F�c�N</summary>
    Public Function CheckSonzai(ByVal strAiteSakiSyubetu As String, ByVal strAiteSakiCd As String, ByVal strSyouhinCd As String, ByVal strTyousaHouhouNo As String) As Data.DataTable

        Return HanbaiKakakuMasterDA.CheckSonzai(strAiteSakiSyubetu, strAiteSakiCd, strSyouhinCd, strTyousaHouhouNo)

    End Function

    ''' <summary>�̔����i�}�X�^�ʐݒ�̓o�^</summary>
    Public Function SetHanbaiKakakuKobeituSettei(ByVal dtHanbaiKakakuOk As Data.DataTable, ByVal strUserId As String) As Boolean

        Return HanbaiKakakuMasterDA.InsUpdHanbaiKakaku(dtHanbaiKakakuOk, strUserId)

    End Function

    '====================2011/05/16 �ԗ� �d�l�ύX �ǉ� �I����===========================


End Class
