Public Class UrikakeDataRecord

#Region "�Ώ۔N��"
    ''' <summary>
    ''' �Ώ۔N��
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTaisyouNengetu As DateTime
    ''' <summary>
    ''' �Ώ۔N��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �Ώ۔N��</returns>
    ''' <remarks></remarks>
    <TableMap("taisyou_nengetu")> _
    Public Property TaisyouNengetu() As DateTime
        Get
            Return dateTaisyouNengetu
        End Get
        Set(ByVal value As DateTime)
            dateTaisyouNengetu = value
        End Set
    End Property
#End Region

#Region "������R�[�h"
    ''' <summary>
    ''' ������R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiCd As String
    ''' <summary>
    ''' ������R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������R�[�h</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_cd")> _
    Public Property SeikyuuSakiCd() As String
        Get
            Return strSeikyuuSakiCd
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiCd = value
        End Set
    End Property
#End Region

#Region "������}��"
    ''' <summary>
    ''' ������}��
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiBrc As String
    ''' <summary>
    ''' ������}��
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������}��</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_brc")> _
    Public Property SeikyuuSakiBrc() As String
        Get
            Return strSeikyuuSakiBrc
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiBrc = value
        End Set
    End Property
#End Region

#Region "������敪"
    ''' <summary>
    ''' ������敪
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiKbn As String
    ''' <summary>
    ''' ������敪
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������敪</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_kbn")> _
    Public Property SeikyuuSakiKbn() As String
        Get
            Return strSeikyuuSakiKbn
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiKbn = value
        End Set
    End Property
#End Region

#Region "�����J�z�c��"
    ''' <summary>
    ''' �����J�z�c��
    ''' </summary>
    ''' <remarks></remarks>
    Private lngTougetuKurikosiZan As Long
    ''' <summary>
    ''' �����J�z�c��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����J�z�c��</returns>
    ''' <remarks></remarks>
    <TableMap("tougetu_kurikosi_zan")> _
    Public Property TougetuKurikosiZan() As Long
        Get
            Return lngTougetuKurikosiZan
        End Get
        Set(ByVal value As Long)
            lngTougetuKurikosiZan = value
        End Set
    End Property
#End Region

#Region "�����z [����]"
    ''' <summary>
    ''' �����z [����]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngGenkin As Long
    ''' <summary>
    ''' �����z [����]
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����z [����]</returns>
    ''' <remarks></remarks>
    <TableMap("genkin")> _
    Public Property Genkin() As Long
        Get
            Return lngGenkin
        End Get
        Set(ByVal value As Long)
            lngGenkin = value
        End Set
    End Property
#End Region

#Region "�����z [���؎�]"
    ''' <summary>
    ''' �����z [���؎�]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngKogitte As Long
    ''' <summary>
    ''' �����z [���؎�]
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����z [���؎�]</returns>
    ''' <remarks></remarks>
    <TableMap("kogitte")> _
    Public Property Kogitte() As Long
        Get
            Return lngKogitte
        End Get
        Set(ByVal value As Long)
            lngKogitte = value
        End Set
    End Property
#End Region

#Region "�����z [�U��]"
    ''' <summary>
    ''' �����z [�U��]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngFurikomi As Long
    ''' <summary>
    ''' �����z [�U��]
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����z [�U��]</returns>
    ''' <remarks></remarks>
    <TableMap("furikomi")> _
    Public Property Furikomi() As Long
        Get
            Return lngFurikomi
        End Get
        Set(ByVal value As Long)
            lngFurikomi = value
        End Set
    End Property
#End Region

#Region "�����z [��`]"
    ''' <summary>
    ''' �����z [��`]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngTegata As Long
    ''' <summary>
    ''' �����z [��`]
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����z [��`]</returns>
    ''' <remarks></remarks>
    <TableMap("tegata")> _
    Public Property Tegata() As Long
        Get
            Return lngTegata
        End Get
        Set(ByVal value As Long)
            lngTegata = value
        End Set
    End Property
#End Region

#Region "�����z [���E]"
    ''' <summary>
    ''' �����z [���E]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngSousai As Long
    ''' <summary>
    ''' �����z [���E]
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����z [���E]</returns>
    ''' <remarks></remarks>
    <TableMap("sousai")> _
    Public Property Sousai() As Long
        Get
            Return lngSousai
        End Get
        Set(ByVal value As Long)
            lngSousai = value
        End Set
    End Property
#End Region

#Region "�����z [�l��]"
    ''' <summary>
    ''' �����z [�l��]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngNebiki As Long
    ''' <summary>
    ''' �����z [�l��]
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����z [�l��]</returns>
    ''' <remarks></remarks>
    <TableMap("nebiki")> _
    Public Property Nebiki() As Long
        Get
            Return lngNebiki
        End Get
        Set(ByVal value As Long)
            lngNebiki = value
        End Set
    End Property
#End Region

#Region "�����z [���̑�]"
    ''' <summary>
    ''' �����z [���̑�]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngSonota As Long
    ''' <summary>
    ''' �����z [���̑�]
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����z [���̑�]</returns>
    ''' <remarks></remarks>
    <TableMap("sonota")> _
    Public Property Sonota() As Long
        Get
            Return lngSonota
        End Get
        Set(ByVal value As Long)
            lngSonota = value
        End Set
    End Property
#End Region

#Region "�����z [���͉��]"
    ''' <summary>
    ''' �����z [���͉��]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngKyouryokuKaihi As Long
    ''' <summary>
    ''' �����z [���͉��]
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����z [���͉��]</returns>
    ''' <remarks></remarks>
    <TableMap("kyouryoku_kaihi")> _
    Public Property KyouryokuKaihi() As Long
        Get
            Return lngKyouryokuKaihi
        End Get
        Set(ByVal value As Long)
            lngKyouryokuKaihi = value
        End Set
    End Property
#End Region

#Region "�����z [�����U��]"
    ''' <summary>
    ''' �����z [�����U��]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngKouzaFurikae As Long
    ''' <summary>
    ''' �����z [�����U��]
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����z [�����U��]</returns>
    ''' <remarks></remarks>
    <TableMap("kouza_furikae")> _
    Public Property KouzaFurikae() As Long
        Get
            Return lngKouzaFurikae
        End Get
        Set(ByVal value As Long)
            lngKouzaFurikae = value
        End Set
    End Property
#End Region

#Region "�����z [�U���萔��]"
    ''' <summary>
    ''' �����z [�U���萔��]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngFurikomiTesuuryou As Long
    ''' <summary>
    ''' �����z [�U���萔��]
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����z [�U���萔��]</returns>
    ''' <remarks></remarks>
    <TableMap("furikomi_tesuuryou")> _
    Public Property FurikomiTesuuryou() As Long
        Get
            Return lngFurikomiTesuuryou
        End Get
        Set(ByVal value As Long)
            lngFurikomiTesuuryou = value
        End Set
    End Property
#End Region

#Region "�����������v"
    ''' <summary>
    ''' �����������v
    ''' </summary>
    ''' <remarks></remarks>
    Private lngTougetuNyuukinGoukei As Long
    ''' <summary>
    ''' �����������v
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����������v</returns>
    ''' <remarks></remarks>
    <TableMap("tougetu_nyuukin_goukei")> _
    Public Property TougetuNyuukinGoukei() As Long
        Get
            Return lngTougetuNyuukinGoukei
        End Get
        Set(ByVal value As Long)
            lngTougetuNyuukinGoukei = value
        End Set
    End Property
#End Region

#Region "�������㍂"
    ''' <summary>
    ''' �������㍂
    ''' </summary>
    ''' <remarks></remarks>
    Private lngTougetuUriageDaka As Long
    ''' <summary>
    ''' �������㍂
    ''' </summary>
    ''' <value></value>
    ''' <returns> �������㍂</returns>
    ''' <remarks></remarks>
    <TableMap("tougetu_uriage_daka")> _
    Public Property TougetuUriageDaka() As Long
        Get
            Return lngTougetuUriageDaka
        End Get
        Set(ByVal value As Long)
            lngTougetuUriageDaka = value
        End Set
    End Property
#End Region

#Region "��������œ�"
    ''' <summary>
    ''' ��������œ�
    ''' </summary>
    ''' <remarks></remarks>
    Private lngTougetuZeiNado As Long
    ''' <summary>
    ''' ��������œ�
    ''' </summary>
    ''' <value></value>
    ''' <returns> ��������œ�</returns>
    ''' <remarks></remarks>
    <TableMap("tougetu_zei_nado")> _
    Public Property TougetuZeiNado() As Long
        Get
            Return lngTougetuZeiNado
        End Get
        Set(ByVal value As Long)
            lngTougetuZeiNado = value
        End Set
    End Property
#End Region

#Region "�o�^���O�C�����[�U�[ID"
    ''' <summary>
    ''' �o�^���O�C�����[�U�[ID
    ''' </summary>
    ''' <remarks></remarks>
    Private strAddLoginUserId As String
    ''' <summary>
    ''' �o�^���O�C�����[�U�[ID
    ''' </summary>
    ''' <value></value>
    ''' <returns> �o�^���O�C�����[�U�[ID</returns>
    ''' <remarks></remarks>
    <TableMap("add_login_user_id")> _
    Public Property AddLoginUserId() As String
        Get
            Return strAddLoginUserId
        End Get
        Set(ByVal value As String)
            strAddLoginUserId = value
        End Set
    End Property
#End Region

#Region "�o�^����"
    ''' <summary>
    ''' �o�^����
    ''' </summary>
    ''' <remarks></remarks>
    Private dateAddDatetime As DateTime
    ''' <summary>
    ''' �o�^����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �o�^����</returns>
    ''' <remarks></remarks>
    <TableMap("add_datetime")> _
    Public Property AddDatetime() As DateTime
        Get
            Return dateAddDatetime
        End Get
        Set(ByVal value As DateTime)
            dateAddDatetime = value
        End Set
    End Property
#End Region

#Region "�X�V���O�C�����[�U�[ID"
    ''' <summary>
    ''' �X�V���O�C�����[�U�[ID
    ''' </summary>
    ''' <remarks></remarks>
    Private strUpdLoginUserId As String
    ''' <summary>
    ''' �X�V���O�C�����[�U�[ID
    ''' </summary>
    ''' <value></value>
    ''' <returns> �X�V���O�C�����[�U�[ID</returns>
    ''' <remarks></remarks>
    <TableMap("upd_login_user_id")> _
    Public Property UpdLoginUserId() As String
        Get
            Return strUpdLoginUserId
        End Get
        Set(ByVal value As String)
            strUpdLoginUserId = value
        End Set
    End Property
#End Region

#Region "�X�V����"
    ''' <summary>
    ''' �X�V����
    ''' </summary>
    ''' <remarks></remarks>
    Private dateUpdDatetime As DateTime
    ''' <summary>
    ''' �X�V����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �X�V����</returns>
    ''' <remarks></remarks>
    <TableMap("upd_datetime")> _
    Public Property UpdDatetime() As DateTime
        Get
            Return dateUpdDatetime
        End Get
        Set(ByVal value As DateTime)
            dateUpdDatetime = value
        End Set
    End Property
#End Region

End Class