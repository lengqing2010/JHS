Public Class JibanSearchRecord

#Region "�敪"
    ''' <summary>
    ''' �敪
    ''' </summary>
    ''' <remarks></remarks>
    Private strKbn As String
    ''' <summary>
    ''' �敪
    ''' </summary>
    ''' <value></value>
    ''' <returns> �敪</returns>
    ''' <remarks></remarks>
    <TableMap("kbn")> _
    Public Property Kbn() As String
        Get
            Return strKbn
        End Get
        Set(ByVal value As String)
            strKbn = value
        End Set
    End Property
#End Region

#Region "�ۏ؏�NO"
    ''' <summary>
    ''' �ۏ؏�NO
    ''' </summary>
    ''' <remarks></remarks>
    Private strHosyousyoNo As String
    ''' <summary>
    ''' �ۏ؏�NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ۏ؏�NO</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_no")> _
    Public Property HosyousyoNo() As String
        Get
            Return strHosyousyoNo
        End Get
        Set(ByVal value As String)
            strHosyousyoNo = value
        End Set
    End Property
#End Region

#Region "�f�[�^�j�����"
    ''' <summary>
    ''' �f�[�^�j�����
    ''' </summary>
    ''' <remarks></remarks>
    Private strDataHakiSyubetu As String
    ''' <summary>
    ''' �f�[�^�j�����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �f�[�^�j�����</returns>
    ''' <remarks></remarks>
    <TableMap("data_haki_syubetu")> _
    Public Property DataHakiSyubetu() As String
        Get
            Return strDataHakiSyubetu
        End Get
        Set(ByVal value As String)
            strDataHakiSyubetu = value
        End Set
    End Property
#End Region

#Region "�{�喼"
    ''' <summary>
    ''' �{�喼
    ''' </summary>
    ''' <remarks></remarks>
    Private strSesyuMei As String
    ''' <summary>
    ''' �{�喼
    ''' </summary>
    ''' <value></value>
    ''' <returns> �{�喼</returns>
    ''' <remarks></remarks>
    <TableMap("sesyu_mei")> _
    Public Property SesyuMei() As String
        Get
            Return strSesyuMei
        End Get
        Set(ByVal value As String)
            strSesyuMei = value
        End Set
    End Property
#End Region

#Region "�����Z��1"
    ''' <summary>
    ''' �����Z��1
    ''' </summary>
    ''' <remarks></remarks>
    Private strBukkenJyuusyo1 As String
    ''' <summary>
    ''' �����Z��1
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����Z��1</returns>
    ''' <remarks></remarks>
    <TableMap("bukken_jyuusyo1")> _
    Public Property BukkenJyuusyo1() As String
        Get
            Return strBukkenJyuusyo1
        End Get
        Set(ByVal value As String)
            strBukkenJyuusyo1 = value
        End Set
    End Property
#End Region

#Region "�����Z��2"
    ''' <summary>
    ''' �����Z��2
    ''' </summary>
    ''' <remarks></remarks>
    Private strBukkenJyuusyo2 As String
    ''' <summary>
    ''' �����Z��2
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����Z��2</returns>
    ''' <remarks></remarks>
    <TableMap("bukken_jyuusyo2")> _
    Public Property BukkenJyuusyo2() As String
        Get
            Return strBukkenJyuusyo2
        End Get
        Set(ByVal value As String)
            strBukkenJyuusyo2 = value
        End Set
    End Property
#End Region

#Region "�����Z��3"
    ''' <summary>
    ''' �����Z��3
    ''' </summary>
    ''' <remarks></remarks>
    Private strBukkenJyuusyo3 As String
    ''' <summary>
    ''' �����Z��3
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����Z��3</returns>
    ''' <remarks></remarks>
    <TableMap("bukken_jyuusyo3")> _
    Public Property BukkenJyuusyo3() As String
        Get
            Return strBukkenJyuusyo3
        End Get
        Set(ByVal value As String)
            strBukkenJyuusyo3 = value
        End Set
    End Property
#End Region

#Region "�����X�R�[�h"
    ''' <summary>
    ''' �����X�R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenCd As String
    ''' <summary>
    ''' �����X�R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����X�R�[�h</returns>
    ''' <remarks></remarks>
    <TableMap("kameiten_cd")> _
    Public Property KameitenCd() As String
        Get
            Return strKameitenCd
        End Get
        Set(ByVal value As String)
            strKameitenCd = value
        End Set
    End Property
#End Region

#Region "�����X���"
    ''' <summary>
    ''' �����X���
    ''' </summary>
    ''' <remarks></remarks>
    Private intKtTorikesi As Integer
    ''' <summary>
    ''' �����X���
    ''' </summary>
    ''' <value></value>
    ''' <returns>�����X���</returns>
    ''' <remarks></remarks>
    <TableMap("kt_torikesi")> _
    Public Property KtTorikesi() As Integer
        Get
            Return intKtTorikesi
        End Get
        Set(ByVal value As Integer)
            intKtTorikesi = value
        End Set
    End Property
#End Region

#Region "�����X������R"
    ''' <summary>
    ''' �����X������R
    ''' </summary>
    ''' <remarks></remarks>
    Private strKtTorikesiRiyuu As String
    ''' <summary>
    ''' �����X������R
    ''' </summary>
    ''' <value></value>
    ''' <returns>�����X������R</returns>
    ''' <remarks></remarks>
    <TableMap("kt_torikesi_riyuu")> _
    Public Property KtTorikesiRiyuu() As String
        Get
            Return strKtTorikesiRiyuu
        End Get
        Set(ByVal value As String)
            strKtTorikesiRiyuu = value
        End Set
    End Property
#End Region

#Region "�����X��1"
    ''' <summary>
    ''' �����X��1
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenMei1 As String
    ''' <summary>
    ''' �����X��1
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����X��1</returns>
    ''' <remarks></remarks>
    <TableMap("kameiten_mei1")> _
    Public Property KameitenMei1() As String
        Get
            Return strKameitenMei1
        End Get
        Set(ByVal value As String)
            strKameitenMei1 = value
        End Set
    End Property
#End Region

#Region "�������{��"
    ''' <summary>
    ''' �������{��
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTysJissiDate As DateTime
    ''' <summary>
    ''' �������{��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �������{��</returns>
    ''' <remarks></remarks>
    <TableMap("tys_jissi_date")> _
    Public Property TysJissiDate() As DateTime
        Get
            Return dateTysJissiDate
        End Get
        Set(ByVal value As DateTime)
            dateTysJissiDate = value
        End Set
    End Property
#End Region

#Region "�˗��S����"
    ''' <summary>
    ''' �˗��S����
    ''' </summary>
    ''' <remarks></remarks>
    Private strIraiTantousyaMei As String
    ''' <summary>
    ''' �˗��S����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �˗��S����</returns>
    ''' <remarks></remarks>
    <TableMap("irai_tantousya_mei")> _
    Public Property IraiTantousyaMei() As String
        Get
            Return strIraiTantousyaMei
        End Get
        Set(ByVal value As String)
            strIraiTantousyaMei = value
        End Set
    End Property
#End Region

#Region "�˗���"
    ''' <summary>
    ''' �˗���
    ''' </summary>
    ''' <remarks></remarks>
    Private dateIraiDate As DateTime
    ''' <summary>
    ''' �˗���
    ''' </summary>
    ''' <value></value>
    ''' <returns> �˗���</returns>
    ''' <remarks></remarks>
    <TableMap("irai_date")> _
    Public Property IraiDate() As DateTime
        Get
            Return dateIraiDate
        End Get
        Set(ByVal value As DateTime)
            dateIraiDate = value
        End Set
    End Property
#End Region

#Region "������]��"
    ''' <summary>
    ''' ������]��
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTysKibouDate As DateTime
    ''' <summary>
    ''' ������]��
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������]��</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kibou_date")> _
    Public Property TysKibouDate() As DateTime
        Get
            Return dateTysKibouDate
        End Get
        Set(ByVal value As DateTime)
            dateTysKibouDate = value
        End Set
    End Property
#End Region

#Region "�\���FLG"
    ''' <summary>
    ''' �\���FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intYoyakuZumiFlg As Integer
    ''' <summary>
    ''' �\���FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns> �\���FLG</returns>
    ''' <remarks></remarks>
    <TableMap("yoyaku_zumi_flg")> _
    Public Property YoyakuZumiFlg() As Integer
        Get
            Return intYoyakuZumiFlg
        End Get
        Set(ByVal value As Integer)
            intYoyakuZumiFlg = value
        End Set
    End Property
#End Region

#Region "������ЃR�[�h"
    ''' <summary>
    ''' ������ЃR�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaCd As String
    ''' <summary>
    ''' ������ЃR�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������ЃR�[�h</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kaisya_cd")> _
    Public Property TysKaisyaCd() As String
        Get
            Return strTysKaisyaCd
        End Get
        Set(ByVal value As String)
            strTysKaisyaCd = value
        End Set
    End Property
#End Region

#Region "������Ў��Ə��R�[�h"
    ''' <summary>
    ''' ������Ў��Ə��R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaJigyousyoCd As String
    ''' <summary>
    ''' ������Ў��Ə��R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������Ў��Ə��R�[�h</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kaisya_jigyousyo_cd")> _
    Public Property TysKaisyaJigyousyoCd() As String
        Get
            Return strTysKaisyaJigyousyoCd
        End Get
        Set(ByVal value As String)
            strTysKaisyaJigyousyoCd = value
        End Set
    End Property
#End Region

#Region "������Ж�"
    ''' <summary>
    ''' ������Ж�
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaMei As String
    ''' <summary>
    ''' ������Ж�
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������Ж�</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kaisya_mei")> _
    Public Property TysKaisyaMei() As String
        Get
            Return strTysKaisyaMei
        End Get
        Set(ByVal value As String)
            strTysKaisyaMei = value
        End Set
    End Property
#End Region

#Region "�������@����"
    ''' <summary>
    ''' �������@����
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysHouhouMei As String
    ''' <summary>
    ''' �������@����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �������@����</returns>
    ''' <remarks></remarks>
    <TableMap("tys_houhou_mei")> _
    Public Property TysHouhouMei() As String
        Get
            Return strTysHouhouMei
        End Get
        Set(ByVal value As String)
            strTysHouhouMei = value
        End Set
    End Property
#End Region

#Region "������������"
    ''' <summary>
    ''' ������������
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSyoudakusyoTysDate As DateTime
    ''' <summary>
    ''' ������������
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������������</returns>
    ''' <remarks></remarks>
    <TableMap("syoudakusyo_tys_date")> _
    Public Property SyoudakusyoTysDate() As DateTime
        Get
            Return dateSyoudakusyoTysDate
        End Get
        Set(ByVal value As DateTime)
            dateSyoudakusyoTysDate = value
        End Set
    End Property
#End Region

#Region "�����H���X�����z"
    ''' <summary>
    ''' �����H���X�����z
    ''' </summary>
    ''' <remarks></remarks>
    Private intKoumutenSeikyuuGaku As Integer
    ''' <summary>
    ''' �����H���X�����z
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����H���X�����z</returns>
    ''' <remarks></remarks>
    <TableMap("koumuten_seikyuu_gaku")> _
    Public Property KoumutenSeikyuuGaku() As Integer
        Get
            Return intKoumutenSeikyuuGaku
        End Get
        Set(ByVal value As Integer)
            intKoumutenSeikyuuGaku = value
        End Set
    End Property
#End Region

#Region "�����������z"
    ''' <summary>
    ''' �����������z
    ''' </summary>
    ''' <remarks></remarks>
    Private intUriGaku As Integer
    ''' <summary>
    ''' �����������z
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����������z</returns>
    ''' <remarks></remarks>
    <TableMap("uri_gaku")> _
    Public Property UriGaku() As Integer
        Get
            Return intUriGaku
        End Get
        Set(ByVal value As Integer)
            intUriGaku = value
        End Set
    End Property
#End Region

#Region "�������������z"
    ''' <summary>
    ''' �������������z
    ''' </summary>
    ''' <remarks></remarks>
    Private intSiireGaku As Integer
    ''' <summary>
    ''' �������������z
    ''' </summary>
    ''' <value></value>
    ''' <returns> �d�����z</returns>
    ''' <remarks></remarks>
    <TableMap("siire_gaku")> _
    Public Property SiireGaku() As Integer
        Get
            Return intSiireGaku
        End Get
        Set(ByVal value As Integer)
            intSiireGaku = value
        End Set
    End Property
#End Region

#Region "�S���Җ�"
    ''' <summary>
    ''' �S���Җ�
    ''' </summary>
    ''' <remarks></remarks>
    Private strTantousyaMei As String
    ''' <summary>
    ''' �S���Җ�
    ''' </summary>
    ''' <value></value>
    ''' <returns> �S���Җ�</returns>
    ''' <remarks></remarks>
    <TableMap("tantousya_mei")> _
    Public Property TantousyaMei() As String
        Get
            Return strTantousyaMei
        End Get
        Set(ByVal value As String)
            strTantousyaMei = value
        End Set
    End Property
#End Region

#Region "���F�Җ��i�H���S���ҁj"
    ''' <summary>
    ''' ���F�Җ��i�H���S���ҁj
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouninsyaMei As String
    ''' <summary>
    ''' ���F�Җ��i�H���S���ҁj
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���F�Җ��i�H���S���ҁj</returns>
    ''' <remarks></remarks>
    <TableMap("syouninsya_mei")> _
    Public Property SyouninsyaMei() As String
        Get
            Return strSyouninsyaMei
        End Get
        Set(ByVal value As String)
            strSyouninsyaMei = value
        End Set
    End Property
#End Region

#Region "����1"
    ''' <summary>
    ''' ����1
    ''' </summary>
    ''' <remarks></remarks>
    Private strHantei1 As String
    ''' <summary>
    ''' ����1
    ''' </summary>
    ''' <value></value>
    ''' <returns> ����1</returns>
    ''' <remarks></remarks>
    <TableMap("hantei1")> _
    Public Property Hantei1() As String
        Get
            Return strHantei1
        End Get
        Set(ByVal value As String)
            strHantei1 = value
        End Set
    End Property
#End Region

#Region "����ڑ�����"
    ''' <summary>
    ''' ����ڑ�����
    ''' </summary>
    ''' <remarks></remarks>
    Private strHanteiSetuzokuMoji As String
    ''' <summary>
    ''' ����ڑ�����
    ''' </summary>
    ''' <value></value>
    ''' <returns> ����ڑ�����</returns>
    ''' <remarks></remarks>
    <TableMap("hantei_setuzoku_moji")> _
    Public Property HanteiSetuzokuMoji() As String
        Get
            Return strHanteiSetuzokuMoji
        End Get
        Set(ByVal value As String)
            strHanteiSetuzokuMoji = value
        End Set
    End Property
#End Region

#Region "����2"
    ''' <summary>
    ''' ����2
    ''' </summary>
    ''' <remarks></remarks>
    Private strHantei2 As String
    ''' <summary>
    ''' ����2
    ''' </summary>
    ''' <value></value>
    ''' <returns> ����2</returns>
    ''' <remarks></remarks>
    <TableMap("hantei2")> _
    Public Property Hantei2() As String
        Get
            Return strHantei2
        End Get
        Set(ByVal value As String)
            strHantei2 = value
        End Set
    End Property
#End Region

#Region "�v�揑�쐬��"
    ''' <summary>
    ''' �v�揑�쐬��
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKeikakusyoSakuseiDate As DateTime
    ''' <summary>
    ''' �v�揑�쐬��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �v�揑�쐬��</returns>
    ''' <remarks></remarks>
    <TableMap("keikakusyo_sakusei_date")> _
    Public Property KeikakusyoSakuseiDate() As DateTime
        Get
            Return dateKeikakusyoSakuseiDate
        End Get
        Set(ByVal value As DateTime)
            dateKeikakusyoSakuseiDate = value
        End Set
    End Property
#End Region

#Region "�ۏ؏����s��"
    ''' <summary>
    ''' �ۏ؏����s��
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHosyousyoHakDate As DateTime
    ''' <summary>
    ''' �ۏ؏����s��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ۏ؏����s��</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_hak_date")> _
    Public Property HosyousyoHakDate() As DateTime
        Get
            Return dateHosyousyoHakDate
        End Get
        Set(ByVal value As DateTime)
            dateHosyousyoHakDate = value
        End Set
    End Property
#End Region

#Region "���l"
    ''' <summary>
    ''' ���l
    ''' </summary>
    ''' <remarks></remarks>
    Private strBikou As String
    ''' <summary>
    ''' ���l
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���l</returns>
    ''' <remarks></remarks>
    <TableMap("bikou")> _
    Public Property Bikou() As String
        Get
            Return strBikou
        End Get
        Set(ByVal value As String)
            strBikou = value
        End Set
    End Property
#End Region

#Region "�c�ƒS����"
    ''' <summary>
    ''' �c�ƒS����
    ''' </summary>
    ''' <remarks></remarks>
    Private strEigyouTantousyaMei As String
    ''' <summary>
    ''' �c�ƒS����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �c�ƒS����</returns>
    ''' <remarks></remarks>
    <TableMap("eigyou_tantousya_mei")> _
    Public Property EigyouTantousyaMei() As String
        Get
            Return strEigyouTantousyaMei
        End Get
        Set(ByVal value As String)
            strEigyouTantousyaMei = value
        End Set
    End Property
#End Region

#Region "�H������N����"
    ''' <summary>
    ''' �H������N����
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKojUriDate As DateTime
    ''' <summary>
    ''' �H������N����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �H������N����</returns>
    ''' <remarks></remarks>
    <TableMap("koj_uri_date")> _
    Public Property KojUriDate() As DateTime
        Get
            Return dateKojUriDate
        End Get
        Set(ByVal value As DateTime)
            dateKojUriDate = value
        End Set
    End Property
#End Region

#Region "�����R�[�h"
    ''' <summary>
    ''' �����R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private intBunjouCd As Integer = Integer.MinValue
    ''' <summary>
    ''' �����R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <TableMap("bunjou_cd")> _
    Public Property BunjouCd() As Integer
        Get
            Return intBunjouCd
        End Get
        Set(ByVal value As Integer)
            intBunjouCd = value
        End Set
    End Property
#End Region

#Region "��������R�[�h"
    ''' <summary>
    ''' ��������R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private strBukkenNayoseCd As String
    ''' <summary>
    ''' ��������R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <TableMap("bukken_nayose_cd")> _
    Public Property BukkenNayoseCd() As String
        Get
            Return strBukkenNayoseCd
        End Get
        Set(ByVal value As String)
            strBukkenNayoseCd = value
        End Set
    End Property
#End Region

#Region "�_��NO"
    ''' <summary>
    ''' �_��NO
    ''' </summary>
    ''' <remarks></remarks>
    Private strKeiyakuNo As String
    ''' <summary>
    ''' �_��NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> �_��NO</returns>
    ''' <remarks></remarks>
    <TableMap("keiyaku_no")> _
    Public Overridable Property KeiyakuNo() As String
        Get
            Return strKeiyakuNo
        End Get
        Set(ByVal value As String)
            strKeiyakuNo = value
        End Set
    End Property
#End Region
End Class