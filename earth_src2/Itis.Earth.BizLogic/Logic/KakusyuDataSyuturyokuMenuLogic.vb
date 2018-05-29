Imports Itis.Earth.DataAccess
Public Class KakusyuDataSyuturyokuMenuLogic

    '�C���X�^���X����
    Private KakusyuDataSyuturyokuMenuDA As New KakusyuDataSyuturyokuMenuDataAccess

    ''' <summary>
    ''' �V�X�e������
    ''' </summary>
    ''' <history>2010/07/19 �n���R(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetSysTime() As String
        Return KakusyuDataSyuturyokuMenuDA.GetSysTime()
    End Function

    ''' <summary>
    ''' Excel�d�󔄏�
    ''' </summary>
    ''' <param name="strDateFrom">���o����FROM</param>
    ''' <param name="strDateTo">���o����TO</param>
    ''' <history>2010/07/19 �n���R(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetExcelSiwakeUriage(ByVal strDateFrom As String, ByVal strDateTo As String) As ExcelSiwakeDataSet.ExcelSiwakeUriageDataTableDataTable
        Return KakusyuDataSyuturyokuMenuDA.GetExcelSiwakeUriage(strDateFrom, strDateTo)
    End Function

    ''' <summary>
    ''' Excel�d��d��
    ''' </summary>
    ''' <param name="strDateFrom">���o����FROM</param>
    ''' <param name="strDateTo">���o����TO</param>
    ''' <history>2010/07/19 �n���R(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetExcelSiwakeSiire(ByVal strDateFrom As String, ByVal strDateTo As String) As ExcelSiwakeDataSet.ExcelSiwakeSiireDataTableDataTable
        Return KakusyuDataSyuturyokuMenuDA.GetExcelSiwakeSiire(strDateFrom, strDateTo)
    End Function

    ''' <summary>
    ''' ���|���c���\
    ''' </summary>
    ''' <param name="strDateFrom">���o����FROM</param>
    ''' <param name="strDateTo">���o����TO</param>
    ''' <history>2010/07/19 �n���R(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetUrikakekinZandakaHyou(ByVal strDateFrom As String, ByVal strDateTo As String) As ExcelSiwakeDataSet.UrikakekinZandakaHyouDataTable
        Return KakusyuDataSyuturyokuMenuDA.GetUrikakekinZandakaHyou(strDateFrom, strDateTo)
    End Function

    ''' <summary>
    ''' Excel�d�����
    ''' </summary>
    ''' <param name="strDateFrom">���o����FROM</param>
    ''' <param name="strDateTo">���o����TO</param>
    ''' <history>2010/07/19 �n���R(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetExcelSiwakeNyuukin(ByVal strDateFrom As String, ByVal strDateTo As String) As ExcelSiwakeDataSet.ExcelSiwakeNyuukinDataTableDataTable
        Return KakusyuDataSyuturyokuMenuDA.GetExcelSiwakeNyuukin(strDateFrom, strDateTo)
    End Function
    ''' <summary>
    ''' ������}�X�^��CSV���擾
    ''' </summary>
    ''' <returns>������}�X�^CSV�e�[�u��</returns>
    ''' <remarks>������}�X�^��CSV���̃f�[�^</remarks>
    ''' <history>2010/07/12 �ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function Getm_seikyuu_sakiCSV() As KakusyuDataSyuturyokuMenuDataSet.m_seikyuu_sakiCSVTableDataTable
        '�f�[�^�擾
        Return KakusyuDataSyuturyokuMenuDA.Selm_seikyuu_sakiCSV()
    End Function

    ''' <summary>
    ''' ������Ѓ}�X�^��CSV���擾
    ''' </summary>
    ''' <returns>������Ѓ}�X�^CSV�e�[�u��</returns>
    ''' <remarks>������Ѓ}�X�^��CSV���̃f�[�^</remarks>
    ''' <history>2010/07/13 �ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function Getm_tyousakaisyaCSV() As KakusyuDataSyuturyokuMenuDataSet.m_tyousakaisyaCSVTableDataTable
        '�f�[�^�擾
        Return KakusyuDataSyuturyokuMenuDA.Selm_tyousakaisyaCSV()
    End Function

    ''' <summary>
    ''' ���i�}�X�^��CSV���擾
    ''' </summary>
    ''' <returns>���i�}�X�^CSV�e�[�u��</returns>
    ''' <remarks>���i�}�X�^��CSV���̃f�[�^</remarks>
    ''' <history>2010/07/13 �ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function Getm_syouhinCSV() As KakusyuDataSyuturyokuMenuDataSet.m_syouhinCSVTableDataTable
        '�f�[�^�擾
        Return KakusyuDataSyuturyokuMenuDA.Selm_syouhinCSV()
    End Function

    ''' <summary>
    ''' ��s�}�X�^��CSV���擾
    ''' </summary>
    ''' <returns>��s�}�X�^CSV�e�[�u��</returns>
    ''' <remarks>��s�}�X�^��CSV���̃f�[�^</remarks>
    ''' <history>2010/07/14 �ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function Getm_ginkouCSV() As KakusyuDataSyuturyokuMenuDataSet.m_ginkouCSVTableDataTable
        '�f�[�^�擾
        Return KakusyuDataSyuturyokuMenuDA.Selm_ginkouCSV()
    End Function

    ''' <summary>
    ''' ���|���c���\csv�o�͂̃f�[�^���擾
    ''' </summary>
    ''' <param name="strDateFrom">���o����FROM</param>
    ''' <param name="strDateTo">���o����TO</param>
    ''' <history>2010/07/20 �Ⓦ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetKaikakekinZandakaHyou(ByVal strDateFrom As String, ByVal strDateTo As String) As ExcelSiwakeDataSet.KaikakekinZandakaHyouDataTable
        '�f�[�^�擾
        Return KakusyuDataSyuturyokuMenuDA.SelKaikakekinZandakaHyou(strDateFrom, strDateTo)
    End Function

    ''' <summary>
    ''' ����f�[�^�o�͂�CSV���擾
    ''' </summary>
    ''' <returns>����f�[�^�o��CSV�e�[�u��</returns>
    ''' <remarks>����f�[�^�o�͂�CSV���̃f�[�^</remarks>
    ''' <history>
    ''' 2010/07/15 �ԗ�(��A���V�X�e����)�@�V�K�쐬
    ''' 2015/03/03 ���h�m(��A���V�X�e����)�@�C��
    ''' </history>
    Public Function Geturiage_data_syuturyokuCSV(ByVal fromDate As String, _
                                             ByVal toDate As String, _
                                             ByVal lstSeikyuuSakiCd As List(Of String), _
                                             ByVal lstSeikyuuSakiBrc As List(Of String), _
                                             ByVal lstSeikyuuSakiKbn As List(Of String) _
                                            ) As KakusyuDataSyuturyokuMenuDataSet.uriage_data_syuturyokuCSVTableDataTable
        'Public Function Geturiage_data_syuturyokuCSV(ByVal fromDate As String, _
        '                                             ByVal toDate As String, _
        '                                             ByVal seikyuu_saki_cd As String, _
        '                                             ByVal seikyuu_saki_brc As String, _
        '                                             ByVal seikyuu_saki_kbn As String _
        '                                            ) As KakusyuDataSyuturyokuMenuDataSet.uriage_data_syuturyokuCSVTableDataTable

        '�f�[�^�擾
        'Return KakusyuDataSyuturyokuMenuDA.Seluriage_data_syuturyokuCSV(fromDate, toDate, seikyuu_saki_cd, seikyuu_saki_brc, seikyuu_saki_kbn)
        Return KakusyuDataSyuturyokuMenuDA.Seluriage_data_syuturyokuCSV(fromDate, toDate, lstSeikyuuSakiCd, lstSeikyuuSakiBrc, lstSeikyuuSakiKbn)
    End Function

    ''' <summary>
    ''' �d���f�[�^�o�͂�CSV���擾
    ''' </summary>
    ''' <returns>�d���f�[�^�o��CSV�e�[�u��</returns>
    ''' <remarks>�d���f�[�^�o�͂�CSV���̃f�[�^</remarks>
    ''' <history>2010/07/16 �ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function Getsiire_data_syuturyokuCSV(ByVal fromDate As String, _
                                                ByVal toDate As String, _
                                                ByVal strSiireCd As String _
                                                ) As KakusyuDataSyuturyokuMenuDataSet.siire_data_syuturyokuCSVTableDataTable

        '�f�[�^�擾
        Return KakusyuDataSyuturyokuMenuDA.Selsiire_data_syuturyokuCSV(fromDate, toDate, strSiireCd)
    End Function
    ''' <summary>
    ''' ������R�[�h�̏���SeikyuuSakiInfoRecord�N���X��List(Of SeikyuuSakiInfoRecord)�Ŏ擾���܂�
    ''' </summary>
    ''' <param name="seikyuuSakiCd">������R�[�h</param>
    ''' <param name="seikyuuSakiBrc">������}��</param>
    ''' <param name="seikyuuSakiKbn">������敪</param>
    ''' <param name="seikyuuSakiMei">�����於</param>
    ''' <param name="seikyuuSakiKana">������J�i</param>
    ''' <returns>SeikyuuSakiInfoRecord�N���X��List</returns>
    ''' <history>2010/07/19 �n���R(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetSeikyuuSakiInfo(ByVal seikyuuSakiCd As String, _
                                       ByVal seikyuuSakiBrc As String, _
                                       ByVal seikyuuSakiKbn As String, _
                                       ByVal seikyuuSakiMei As String, _
                                       ByVal seikyuuSakiKana As String, _
                                       ByRef allCount As Integer, _
                                       Optional ByVal startRow As Integer = 1, _
                                       Optional ByVal endRow As Integer = 99999999, _
                                       Optional ByVal blnTorikesi As Boolean = False) As DataTable

        Return KakusyuDataSyuturyokuMenuDA.searchSeikyuuSakiInfo(seikyuuSakiCd, seikyuuSakiBrc, seikyuuSakiKbn, seikyuuSakiMei, seikyuuSakiKana, blnTorikesi)

    End Function

    ''' <summary>
    ''' ������Ѓ}�X�^���������^�C�v
    ''' </summary>
    ''' <history>2010/07/19 �n���R(��A���V�X�e����)�@�V�K�쐬</history>
    Enum EnumTyousakaisyaKensakuType
        ''' <summary>
        ''' �������
        ''' </summary>
        ''' <remarks></remarks>
        TYOUSAKAISYA = 0
        ''' <summary>
        ''' �d����
        ''' </summary>
        ''' <remarks></remarks>
        SIIRESAKI = 1
        ''' <summary>
        ''' �x����
        ''' </summary>
        ''' <remarks></remarks>
        SIHARAISAKI = 2
        ''' <summary>
        ''' �H�����
        ''' </summary>
        ''' <remarks></remarks>
        KOUJIKAISYA = 3
    End Enum

    ''' <summary>
    ''' ������Ѓ}�X�^����
    ''' </summary>
    ''' <param name="strTysKaiCd">������к���</param>
    ''' <param name="strJigyousyoCd">���Ə�����</param>
    ''' <param name="strTysKaiNm">������Ж�</param>
    ''' <param name="strTysKaiKana">������Ж���</param>
    ''' <param name="blnDelete">����Ώۃt���O</param>
    ''' <param name="kameitenCd">�����X�R�[�h([�C��]�ۋ敪�`�F�b�N�p)</param>
    ''' <param name="kensakuType">�����^�C�v([�C��](EnumTyousakaisyakensakuType))</param>
    ''' <returns></returns>
    ''' <history>2010/07/19 �n���R(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetTyousakaisyaSearchResult(ByVal strTysKaiCd As String, _
                                                ByVal strJigyousyoCd As String, _
                                                ByVal strTysKaiNm As String, _
                                                ByVal strTysKaiKana As String, _
                                                ByVal blnDelete As Boolean, _
                                                Optional ByVal kameitenCd As String = "", _
                                                Optional ByVal kensakuType As EnumTyousakaisyaKensakuType = EnumTyousakaisyaKensakuType.TYOUSAKAISYA _
                                                ) As DataTable


        Return KakusyuDataSyuturyokuMenuDA.GetTyousakaisyaKensakuData(strTysKaiCd, _
                                                                    strJigyousyoCd, _
                                                                    strTysKaiNm, _
                                                                    strTysKaiKana, _
                                                                    blnDelete, _
                                                                    kameitenCd, _
                                                                    kensakuType _
                                                                    )
    End Function

End Class
