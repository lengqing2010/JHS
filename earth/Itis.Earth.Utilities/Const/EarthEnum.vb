''' <summary>
''' �񋓌^���i�[���܂�
''' </summary>
''' <remarks></remarks>
Public Class EarthEnum

#Region "���i�敪���"
    ''' <summary>
    ''' ���i�敪���
    ''' </summary>
    ''' <remarks></remarks>
    Enum EnumSyouhinKubun
        ''' <summary>
        ''' �S���i
        ''' </summary>
        ''' <remarks></remarks>
        AllSyouhin = -1
        ''' <summary>
        ''' �S���i(����f�[�^�܂�)
        ''' </summary>
        ''' <remarks></remarks>
        AllSyouhinTorikesi = -2
        ''' <summary>
        ''' ���i�敪2
        ''' </summary>
        ''' <remarks></remarks>
        Syouhin1 = 100
        ''' <summary>
        ''' ���i�敪2
        ''' </summary>
        ''' <remarks></remarks>
        Syouhin2_110 = 110
        ''' <summary>
        ''' ���i�敪2
        ''' </summary>
        ''' <remarks></remarks>
        Syouhin2_115 = 115
        ''' <summary>
        ''' ���i�敪3
        ''' </summary>
        ''' <remarks></remarks>
        Syouhin3 = 120
        ''' <summary>
        ''' ���i�敪4
        ''' </summary>
        ''' <remarks></remarks>
        Syouhin4 = 190
        ''' <summary>
        ''' ���ǍH��
        ''' </summary>
        ''' <remarks></remarks>
        KairyouKouji = 130
        ''' <summary>
        ''' �ǉ��H��
        ''' </summary>
        ''' <remarks></remarks>
        TuikaKouji = 140
        ''' <summary>
        ''' �����񍐏�
        ''' </summary>
        ''' <remarks></remarks>
        TyousaHoukokusyo = 150
        ''' <summary>
        ''' �H���񍐏�
        ''' </summary>
        ''' <remarks></remarks>
        KoujiHoukokusyo = 160
        ''' <summary>
        ''' �ۏ؏�
        ''' </summary>
        ''' <remarks></remarks>
        Hosyousyo = 170
        ''' <summary>
        ''' ��񕥖�
        ''' </summary>
        ''' <remarks></remarks>
        Kaiyaku = 180
        ''' <summary>
        ''' �o�^��
        ''' </summary>
        ''' <remarks></remarks>
        TourokuRyou = 200
        ''' <summary>
        ''' �̑��i�����c�[����
        ''' </summary>
        ''' <remarks></remarks>
        ToolRyou = 210
        ''' <summary>
        ''' FC�ȊO�̑��i
        ''' </summary>
        ''' <remarks></remarks>
        HansokuNotFc = 220
        ''' <summary>
        ''' FC�̑��i
        ''' </summary>
        ''' <remarks></remarks>
        HansokuFc = 230
    End Enum
#End Region

#Region "�����^�C�v"
    ''' <summary>
    ''' �����^�C�v
    ''' </summary>
    ''' <remarks></remarks>
    Enum EnumSeikyuuType

        ''' <summary>
        ''' ���ڐ���(�R�n��)
        ''' </summary>
        ''' <remarks></remarks>
        TyokusetuSeikyuuKeiretu = 0

        ''' <summary>
        ''' ������(�R�n��)
        ''' </summary>
        ''' <remarks></remarks>
        TaSeikyuuKeiretu = 1

        ''' <summary>
        ''' ���ڐ���(�R�n��ȊO)
        ''' </summary>
        ''' <remarks></remarks>
        TyokusetuSeikyuuNotKeiretu = 2

        ''' <summary>
        ''' ������(�R�n��ȊO)
        ''' </summary>
        ''' <remarks></remarks>
        TaSeikyuuNotKeiretu = 3

    End Enum
#End Region

#Region "�����X������^�C�v"
    ''' <summary>
    ''' �����X������^�C�v
    ''' </summary>
    ''' <remarks></remarks>
    Enum EnumKameitenSeikyuuType
        ''' <summary>
        ''' �H��������
        ''' </summary>
        ''' <remarks></remarks>
        KoujiSeikyuusaki = 0
        ''' <summary>
        ''' �̑��i������
        ''' </summary>
        ''' <remarks></remarks>
        HansokuhinSeikyuusaki = 1
    End Enum
#End Region

#Region "�V�K�o�^���敪�^�C�v"
    ''' <summary>
    ''' �V�K�o�^���敪�^�C�v
    ''' </summary>
    ''' <remarks>�n�Ճf�[�^��V�K�ǉ�������ʏ����Ǘ�����</remarks>
    Enum EnumSinkiTourokuMotoKbnType
        ''' <summary>
        ''' �n��S:0(or NULL)
        ''' </summary>
        ''' <remarks></remarks>
        JibanS = 0
        ''' <summary>
        ''' EARTH��:1
        ''' </summary>
        ''' <remarks></remarks>
        EarthJyutyuu = 1
        ''' <summary>
        ''' EARTH�\������:2
        ''' </summary>
        ''' <remarks></remarks>
        EarthMousikomi = 2
        ''' <summary>
        ''' ReportJHS(�r���_�[�\��):3
        ''' </summary>
        ''' <remarks></remarks>
        ReportJHS = 3
        ''' <summary>
        ''' ReportJHS(FC�\��):4
        ''' </summary>
        ''' <remarks></remarks>
        ReportJHS_FC = 4
    End Enum
#End Region

#Region "�h���b�v�_�E���^�C�v"
    ''' <summary>
    ''' �h���b�v�_�E���^�C�v
    ''' </summary>
    ''' <remarks>����M(���̎��or�ȊO),�g������M���̔��f���s�Ȃ�</remarks>
    Enum emDdlType
        ''' <summary>
        ''' ����M.���̎��:0
        ''' </summary>
        ''' <remarks></remarks>
        MMeisyouSyubetu = 0
        ''' <summary>
        ''' ����M.���̎�ʈȊO:1
        ''' </summary>
        ''' <remarks></remarks>
        MExcpMeisyouSyubetu = 1
        ''' <summary>
        ''' �g������M:2
        ''' </summary>
        ''' <remarks></remarks>
        KtMeisyou = 2
    End Enum
#End Region

#Region "�g������M�h���b�v�_�E���^�C�v"
    ''' <summary>
    ''' �g������M�h���b�v�_�E���^�C�v
    ''' </summary>
    ''' <remarks></remarks>
    Enum emKtMeisyouType
        ''' <summary>
        ''' �ėp�R�[�h:1
        ''' </summary>
        ''' <remarks></remarks>
        HannyouCd = 1
        ''' <summary>
        ''' �ėpNO:2
        ''' </summary>
        ''' <remarks></remarks>
        HannyouNo = 2
    End Enum
#End Region

#Region "��ʃ��[�h����p"
    ''' <summary>
    ''' ��ʃ��[�h����p
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emGamenMode
        ''' <summary>
        ''' �V�K�F0
        ''' </summary>
        ''' <remarks></remarks>
        SINKI = 0
        ''' <summary>
        ''' �X�V�F1
        ''' </summary>
        ''' <remarks></remarks>
        KOUSIN = 1
        ''' <summary>
        ''' �m�F�F2
        ''' </summary>
        ''' <remarks></remarks>
        KAKUNIN = 2
    End Enum
#End Region

#Region "������Ѓ}�X�^���������^�C�v"
    ''' <summary>
    ''' ������Ѓ}�X�^���������^�C�v
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EnumTyousakaisyaKensakuType
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
#End Region

#Region "�����X������Аݒ�}�X�^���������^�C�v"
    ''' <summary>
    ''' �����X������Аݒ�}�X�^���������^�C�v
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EnumKameitenTyousakaisyaKensakuType
        ''' <summary>
        ''' �w�蒲�����
        ''' </summary>
        ''' <remarks></remarks>
        SITEITYOUSAKAISYA = 0
        ''' <summary>
        ''' �D�撲�����
        ''' </summary>
        ''' <remarks></remarks>
        YUUSENTYOUSAKAISYA = 1
    End Enum
#End Region

#Region "���i�敪�R"
    ''' <summary>
    ''' ���i�敪�R
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emSyouhinKbn3
        ''' <summary>
        ''' ����������
        ''' </summary>
        ''' <remarks></remarks>
        TYOUSA = 1
        ''' <summary>
        ''' �H��������
        ''' </summary>
        ''' <remarks></remarks>
        KOUJI = 2
        ''' <summary>
        ''' �̑��i������
        ''' </summary>
        ''' <remarks></remarks>
        HANSOKUHIN = 3
    End Enum
#End Region

#Region "�������֘A"

#Region "�����f�[�^�����^�C�v"
    ''' <summary>
    ''' �����f�[�^�����^�C�v
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emSeikyuuSearchType
        ''' <summary>
        ''' �������ꗗ���
        ''' </summary>
        ''' <remarks></remarks>
        SearchSeikyuusyo = 1
        ''' <summary>
        ''' �ߋ��������ꗗ���
        ''' </summary>
        ''' <remarks></remarks>
        KakoSearchSeikyuusyo = 2
        ''' <summary>
        ''' �������󎚓��e�ҏW���
        ''' </summary>
        ''' <remarks></remarks>
        SeikyuusyoSyuusei = 3
        ''' <summary>
        ''' ���������ߓ������Ɖ���
        ''' </summary>
        ''' <remarks></remarks>
        SeikyuusyoSimeDateRireki = 4
    End Enum
#End Region

#Region "�����f�[�^�X�V�^�C�v"
    ''' <summary>
    ''' �����f�[�^�X�V�^�C�v
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emSeikyuusyoUpdType
        ''' <summary>
        ''' �������󎚓��e�C��(�������󎚓��e�ҏW���)
        ''' </summary>
        ''' <remarks></remarks>
        SeikyuusyoSyuusei = 1
        ''' <summary>
        ''' ���������(�������ꗗ���)
        ''' </summary>
        ''' <remarks></remarks>
        SeikyuusyoPrint = 2
        ''' <summary>
        ''' ���������(�������ꗗ���)
        ''' </summary>
        ''' <remarks></remarks>
        SeikyuusyoTorikesi = 3
    End Enum
#End Region

#Region "�������ꗗ��ʁE���s�{�^���^�C�v"
    ''' <summary>
    ''' �������ꗗ��ʁE���s�{�^���^�C�v
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emSearchSeikyuusyoBtnType
        ''' <summary>
        ''' �������s
        ''' </summary>
        ''' <remarks></remarks>
        Search = 0
        ''' <summary>
        ''' CSV�o��
        ''' </summary>
        ''' <remarks></remarks>
        CsvOutput = 1
        ''' <summary>
        ''' ���������
        ''' </summary>
        ''' <remarks></remarks>
        Print = 2
        ''' <summary>
        ''' ���������
        ''' </summary>
        ''' <remarks></remarks>
        Torikesi = 3
    End Enum
#End Region

#Region "���������������Ɖ��ʁE���s�{�^���^�C�v"
    ''' <summary>
    ''' ���������������Ɖ��ʁE���s�{�^���^�C�v
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emSearchSeikyuuSimeDateRirekiBtnType
        ''' <summary>
        ''' �������s
        ''' </summary>
        ''' <remarks></remarks>
        Search = 0
        ''' <summary>
        ''' �������
        ''' </summary>
        ''' <remarks></remarks>
        Torikesi = 1
    End Enum
#End Region

#End Region

#Region "�ۏ؏��֘A"

#Region "�����i���󋵉�ʃ��[�h"
    ''' <summary>
    ''' �����i���󋵉�ʃ��[�h
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emBukkenSintyokuJykyGamenMode
        ''' <summary>
        ''' �ŏI��������(����)���
        ''' </summary>
        ''' <remarks></remarks>
        Nitiji = 1
        ''' <summary>
        ''' �ŏI��������(����)���
        ''' </summary>
        ''' <remarks></remarks>
        Getuji = 2
    End Enum
#End Region

#End Region

#Region "���i1�ݒ�G���["

    Public Enum emSyouhin1Error
        ''' <summary>
        ''' ���i�ݒ�ꏊ�擾�G���[
        ''' </summary>
        ''' <remarks></remarks>
        GetKakakuSetteiBasyo = 1
        ''' <summary>
        ''' ���i1���擾�G���[
        ''' </summary>
        ''' <remarks></remarks>
        GetSyouhin1 = 2
        ''' <summary>
        ''' �����X�}�X�^���i�擾���i�ΏۊO�G���[
        ''' </summary>
        ''' <remarks></remarks>
        KameiSyouhinTaisyougai = 3
        ''' <summary>
        ''' �����X�}�X�^���i�擾�G���[
        ''' </summary>
        ''' <remarks></remarks>
        GetKameiKakaku = 4
        ''' <summary>
        ''' �����}�X�^�擾�G���[
        ''' </summary>
        ''' <remarks></remarks>
        GetGenka = 5
        ''' <summary>
        ''' �����T�v�擾�G���[
        ''' </summary>
        ''' <remarks></remarks>
        GetTysGaiyou = 6
        ''' <summary>
        ''' �̔����i�}�X�^�擾�G���[
        ''' </summary>
        ''' <remarks></remarks>
        GetHanbai = 7
        ''' <summary>
        ''' �����E�̔����i�}�X�^�擾�G���[
        ''' </summary>
        ''' <remarks></remarks>
        GetGenkaHanbai = 8
    End Enum

#End Region

#Region "���ʑΉ��֘A"

#Region "���ʑΉ���ʁE���s�{�^���^�C�v"
    ''' <summary>
    ''' ���ʑΉ���ʁE���s�{�^���^�C�v
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emTokubetuTaiouActBtn
        ''' <summary>
        ''' �����Ǎ�����
        ''' </summary>
        ''' <remarks></remarks>
        BtnLoad = -1
        ''' <summary>
        ''' �}�X�^�Ď擾�{�^��
        ''' </summary>
        ''' <remarks></remarks>
        BtnMaster = 0
        ''' <summary>
        ''' �}�X�^�Ď擾�{�^���ȊO
        ''' </summary>
        ''' <remarks></remarks>
        BtnOther = 1
        ''' <summary>
        ''' �o�^�{�^��������
        ''' </summary>
        ''' <remarks></remarks>
        PressBtnMstTouroku = 9
    End Enum
#End Region

#Region "���ʑΉ������^�C�v"
    ''' <summary>
    ''' ���ʑΉ������^�C�v
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emTokubetuTaiouSearchType
        ''' <summary>
        ''' ���ݒ�
        ''' </summary>
        ''' <remarks></remarks>
        None = 0
        ''' <summary>
        ''' �˗��m�F���
        ''' </summary>
        ''' <remarks></remarks>
        IraiKakunin = 1
        ''' <summary>
        ''' �@�ʏC�����
        ''' </summary>
        ''' <remarks></remarks>
        TeibetuSyuusei = 2
    End Enum

#End Region

#End Region

#Region "�\���֘A"

#Region "�\��������ʁE���s�{�^���^�C�v"
    ''' <summary>
    ''' �\��������ʁE���s�{�^���^�C�v
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emSearchMousikomiBtnType
        ''' <summary>
        ''' �������s
        ''' </summary>
        ''' <remarks></remarks>
        Search = 0
        ''' <summary>
        ''' �V�K��
        ''' </summary>
        ''' <remarks></remarks>
        SinkiJutyuu = 1
    End Enum

#End Region

#Region "(FC)�\���C����ʁE(FC)�\���f�[�^�X�V�^�C�v"
    ''' <summary>
    ''' (FC)�\���C����ʁE(FC)�\���f�[�^�X�V�^�C�v
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emMousikomiUpdType
        ''' <summary>
        ''' ���ݒ�(Default)
        ''' </summary>
        ''' <remarks></remarks>
        Misettei = 0
        ''' <summary>
        ''' �C���{�^��
        ''' </summary>
        ''' <remarks></remarks>
        Syuusei = 1
        ''' <summary>
        ''' �ۗ��{�^��
        ''' </summary>
        ''' <remarks></remarks>
        Horyuu = 2
        ''' <summary>
        ''' �V�K�󒍃{�^��
        ''' </summary>
        ''' <remarks></remarks>
        SinkiJutyuu = 3
    End Enum
#End Region

#Region "�\���V�K�󒍃^�C�v"
    ''' <summary>
    ''' �\���V�K�󒍃^�C�v
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emMousikomiSinkiJutyuuType
        ''' <summary>
        ''' (FC)�\���������
        ''' </summary>
        ''' <remarks></remarks>
        SearchMousikomi = 1
        ''' <summary>
        ''' (FC)�\���C�����
        ''' </summary>
        ''' <remarks></remarks>
        MousikomiSyuusei = 2
    End Enum
#End Region

#End Region

#Region "�����N�����ꊇ�ύX�����^�C�v"
    ''' <summary>
    ''' �����N�����ꊇ�ύX�����^�C�v
    ''' </summary>
    ''' <remarks>�Ώۃf�[�^����������e�[�u��</remarks>
    Enum emIkkatuHenkouDataSearchType
        ''' <summary>
        ''' �@�ʐ����e�[�u��
        ''' </summary>
        ''' <remarks></remarks>
        TeibetuSeikyuu = 1
        ''' <summary>
        ''' �X�ʐ����e�[�u��
        ''' </summary>
        ''' <remarks></remarks>
        TenbetuSeikyuu = 2
        ''' <summary>
        ''' �X�ʏ��������e�[�u��
        ''' </summary>
        ''' <remarks></remarks>
        TenbetuSyoki = 3
        ''' <summary>
        ''' �ėp����e�[�u��
        ''' </summary>
        ''' <remarks></remarks>
        HannyouUriage = 4
        ''' <summary>
        ''' ����e�[�u��
        ''' </summary>
        ''' <remarks></remarks>
        UriageData = 5
        ''' <summary>
        ''' ����e�[�u��(���)
        ''' </summary>
        ''' <remarks></remarks>
        UriageDataTorikesiSeikyuuDate = 6

    End Enum
#End Region

#Region "�H�����i�֘A"
    ''' <summary>
    ''' �H�����i�擾����
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emKoujiKakaku
        ''' <summary>
        ''' �H�����i�i�����X�j
        ''' </summary>
        ''' <remarks></remarks>
        Kameiten = 1
        ''' <summary>
        ''' �H�����i�i�c�Ə��j
        ''' </summary>
        ''' <remarks></remarks>
        Eigyousyo = 2
        ''' <summary>
        ''' �H�����i�i�n��j
        ''' </summary>
        ''' <remarks></remarks>
        Keiretu = 3
        ''' <summary>
        ''' �H�����i�i�w�薳�j
        ''' </summary>
        ''' <remarks></remarks>
        SiteiNasi = 4
        ''' <summary>
        ''' ���H�����̑�(�H�����iM�ɑg�ݍ��킹��)
        ''' </summary>
        ''' <remarks></remarks>
        TyokuKojSonota = 5
        ''' <summary>
        ''' ���i�}�X�^
        ''' </summary>
        ''' <remarks></remarks>
        Syouhin = 6
        ''' <summary>
        ''' �p�����[�^�s��
        ''' </summary>
        ''' <remarks></remarks>
        PrmError = 7
    End Enum

    ''' <summary>
    ''' ���s���A�N�V�����^�C�v
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emKojKkkActionType
        ''' <summary>
        ''' �����X�R�[�h
        ''' </summary>
        ''' <remarks></remarks>
        KameitenCd = 0

        ''' <summary>
        ''' �H����ЃR�[�h
        ''' </summary>
        ''' <remarks></remarks>
        KojKaisyaCd = 1

        ''' <summary>
        ''' ���i�R�[�h
        ''' </summary>
        ''' <remarks></remarks>
        SyouhinCd = 2

        ''' <summary>
        ''' �����L��
        ''' </summary>
        ''' <remarks></remarks>
        SeikyuuUmu = 3
    End Enum
#End Region

#Region "�������f�[�^�쐬�֘A"
    ''' <summary>
    ''' ���t�擾�G���[�^�C�v
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emHidukeSyutokuErr

        ''' <summary>
        ''' ����n
        ''' </summary>
        ''' <remarks></remarks>
        OK = 0

        ''' <summary>
        ''' ���t�擾�G���[
        ''' </summary>
        ''' <remarks></remarks>
        SyutokuErr = 1

        ''' <summary>
        ''' SQL�G���[
        ''' </summary>
        ''' <remarks></remarks>
        SqlErr = 2

        ''' <summary>
        ''' ���t�`���G���[
        ''' </summary>
        ''' <remarks></remarks>
        HidukeErr = 3
    End Enum

#End Region

#Region "���ʑΉ����i�Ή�"
    ''' <summary>
    ''' �@�ʐ����̕���
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emTeibetuBunrui
        ''' <summary>
        ''' ���i�ݒ�Ȃ�
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN_NOTHING = 0
        ''' <summary>
        ''' ���i1
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN1 = 1
        ''' <summary>
        ''' ���i2
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN2 = 2
        ''' <summary>
        ''' ���i3
        ''' </summary>
        ''' <remarks></remarks>
        SYOUHIN3 = 3
    End Enum

    ''' <summary>
    ''' �@�ʐ����̐ݒ��ւ̃A�N�V����
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emTeibetuAction
        ''' <summary>
        ''' �Ȃ�
        ''' </summary>
        ''' <remarks></remarks>
        TEIBETU_NOTHING = 0
        ''' <summary>
        ''' ���i�ǉ�
        ''' </summary>
        ''' <remarks></remarks>
        TEIBETU_ADD = 1
        ''' <summary>
        ''' ���i�X�V
        ''' </summary>
        ''' <remarks></remarks>
        TEIBETU_UPD = 2
        ''' <summary>
        ''' ���i3�փZ�b�g
        ''' </summary>
        ''' <remarks></remarks>
        TEIBETU_SYOUHIN_3_SET = 3
    End Enum

    ''' <summary>
    ''' ���z�̃A�N�V����
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emKingakuAction
        ''' <summary>
        ''' �G���[
        ''' </summary>
        ''' <remarks></remarks>
        KINGAKU_ERROR = -2
        ''' <summary>
        ''' �x��
        ''' </summary>
        ''' <remarks></remarks>
        KINGAKU_ALERT = -1
        ''' <summary>
        ''' �Ȃ�
        ''' </summary>
        ''' <remarks></remarks>
        KINGAKU_NOT_ACTION = 0
        ''' <summary>
        ''' ���z
        ''' </summary>
        ''' <remarks></remarks>
        KINGAKU_PLUS = 1
        ''' <summary>
        ''' ���z
        ''' </summary>
        ''' <remarks></remarks>
        KINGAKU_MINUS = 2
        ''' <summary>
        ''' ���z���z
        ''' </summary>
        ''' <remarks></remarks>
        KINGAKU_MINUS_PLUS = 3

    End Enum

    ''' <summary>
    ''' ���ʑΉ����i���f�G���[
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emKkkHanneiErr
        ''' <summary>
        ''' �G���[�Ȃ�
        ''' </summary>
        ''' <remarks></remarks>
        ERR_NOTHING = 0
        ''' <summary>
        ''' ���Z���z�G���[(0�~�ANULL�Ȃ�)
        ''' </summary>
        ''' <remarks></remarks>
        KASAN_KINGAKU_ERR = 1
        ''' <summary>
        ''' ��L�ȊO�̃G���[
        ''' </summary>
        ''' <remarks></remarks>
        ERR_OTHER = 2
    End Enum

    ''' <summary>
    ''' ���i1,2,3�ύX���f
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emTeibetuSeikyuuKkkJyky
        ''' <summary>
        ''' �ύX�Ȃ�:0
        ''' </summary>
        ''' <remarks></remarks>
        NONE = 0
        ''' <summary>
        ''' �ύX����(�ǉ�):1
        ''' </summary>
        ''' <remarks></remarks>
        INSERT = 1
        ''' <summary>
        ''' �ύX����(�X�V):2
        ''' </summary>
        ''' <remarks></remarks>
        UPDATE = 2
        ''' <summary>
        ''' �ύX����(�폜):3
        ''' </summary>
        ''' <remarks></remarks>
        DELETE = 3
    End Enum

    ''' <summary>
    ''' �R�[�h�A���̂̕\���ݒ�
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emGetDispType
        ''' <summary>
        ''' �R�[�h
        ''' </summary>
        ''' <remarks></remarks>
        CODE = 1
        ''' <summary>
        ''' ����
        ''' </summary>
        ''' <remarks></remarks>
        MEISYOU = 2
        ''' <summary>
        ''' �R�[�h":"����
        ''' </summary>
        ''' <remarks></remarks>
        CODE_MEISYOU = 3
    End Enum

    ''' <summary>
    ''' �c�[���`�b�v�\���^�C�v
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emToolTipType
        ''' <summary>
        ''' �\���ΏۊO
        ''' </summary>
        ''' <remarks></remarks>
        NASI = 0
        ''' <summary>
        ''' �����\��
        ''' </summary>
        ''' <remarks></remarks>
        ARI = 1
        ''' <summary>
        ''' ��C��\��
        ''' </summary>
        ''' <remarks></remarks>
        SYUSEI = 2
    End Enum
#End Region

#Region "���i1,2,3�U������"
    ''' <summary>
    ''' ��ʕ\��NO
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emGamenHyoujiNo
        ''' <summary>
        ''' ��ʕ\��NO = 0:���ݒ�
        ''' </summary>
        ''' <remarks></remarks>
        HYOUJI_NOTHING = 0
        ''' <summary>
        ''' ��ʕ\��NO = 1
        ''' </summary>
        ''' <remarks></remarks>
        HYOUJI_NO_1 = 1
        ''' <summary>
        ''' ��ʕ\��NO = 2
        ''' </summary>
        ''' <remarks></remarks>
        HYOUJI_NO_2 = 2
        ''' <summary>
        ''' ��ʕ\��NO = 3
        ''' </summary>
        ''' <remarks></remarks>
        HYOUJI_NO_3 = 3
        ''' <summary>
        ''' ��ʕ\��NO = 4
        ''' </summary>
        ''' <remarks></remarks>
        HYOUJI_NO_4 = 4
        ''' <summary>
        ''' ��ʕ\��NO = 5
        ''' </summary>
        ''' <remarks></remarks>
        HYOUJI_NO_5 = 5
        ''' <summary>
        ''' ��ʕ\��NO = 6
        ''' </summary>
        ''' <remarks></remarks>
        HYOUJI_NO_6 = 6
        ''' <summary>
        ''' ��ʕ\��NO = 7
        ''' </summary>
        ''' <remarks></remarks>
        HYOUJI_NO_7 = 7
        ''' <summary>
        ''' ��ʕ\��NO = 8
        ''' </summary>
        ''' <remarks></remarks>
        HYOUJI_NO_8 = 8
        ''' <summary>
        ''' ��ʕ\��NO = 9
        ''' </summary>
        ''' <remarks></remarks>
        HYOUJI_NO_9 = 9
    End Enum
#End Region

#Region "��ʏ��"
    ''' <summary>
    ''' ��ʏ��
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum emGamenInfo
        ''' <summary>
        ''' �ݒ�Ȃ�
        ''' </summary>
        ''' <remarks></remarks>
        None = 0
        ''' <summary>
        ''' �\������
        ''' </summary>
        ''' <remarks></remarks>
        MousikomiInput = 1
        ''' <summary>
        ''' �\������
        ''' </summary>
        ''' <remarks></remarks>
        MousikomiSearch = 2
        ''' <summary>
        ''' �\������/�C��
        ''' </summary>
        ''' <remarks></remarks>
        MousikomiSyuusei = 3
        ''' <summary>
        ''' ��
        ''' </summary>
        ''' <remarks></remarks>
        Jutyuu = 4
        ''' <summary>
        ''' �ꊇ�ύX�y�������i���z
        ''' </summary>
        ''' <remarks></remarks>
        IkkatuTysSyouhinInfo = 5
        ''' <summary>
        ''' �@�ʃf�[�^�C��
        ''' </summary>
        ''' <remarks></remarks>
        TeibetuSyuusei = 6
        ''' <summary>
        ''' ���ʑΉ�
        ''' </summary>
        ''' <remarks></remarks>
        TokubetuTaiou = 7
        ''' <summary>
        ''' ���ǍH��
        ''' </summary>
        ''' <remarks></remarks>
        KairyouKouji = 8
        ''' <summary>
        ''' �ۏ�
        ''' </summary>
        ''' <remarks></remarks>
        Hosyou = 9
        ''' <summary>
        ''' �񍐏�
        ''' </summary>
        ''' <remarks></remarks>
        Houkokusyo = 10
        ''' <summary>
        ''' FC�\������
        ''' </summary>
        ''' <remarks></remarks>
        FcMousikomiSearch = 11
        ''' <summary>
        ''' FC�\���C��
        ''' </summary>
        ''' <remarks></remarks>
        FcMousikomiSyuusei = 12
    End Enum
#End Region

#Region "���s�{�^���^�C�v"
    ''' <summary>
    ''' ���s�{�^���^�C�v
    ''' </summary>
    ''' <remarks></remarks>
    Enum emExecBtnType
        ''' <summary>
        ''' ����
        ''' </summary>
        ''' <remarks></remarks>
        BtnUriage = 1
        ''' <summary>
        ''' �d��
        ''' </summary>
        ''' <remarks></remarks>
        BtnSiire = 2
        ''' <summary>
        ''' ����
        ''' </summary>
        ''' <remarks></remarks>
        BtnNyuukin = 3
    End Enum
#End Region

End Class
