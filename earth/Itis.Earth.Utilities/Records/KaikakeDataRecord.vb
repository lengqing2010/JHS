Public Class KaikakeDataRecord

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

#Region "�x���W�v�掖�Ə��R�[�h"
    ''' <summary>
    ''' �x���W�v�掖�Ə��R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private strShriJigyousyoCd As String
    ''' <summary>
    ''' �x���W�v�掖�Ə��R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> �x���W�v�掖�Ə��R�[�h</returns>
    ''' <remarks></remarks>
    <TableMap("shri_jigyousyo_cd")> _
    Public Property ShriJigyousyoCd() As String
        Get
            Return strShriJigyousyoCd
        End Get
        Set(ByVal value As String)
            strShriJigyousyoCd = value
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

#Region "�����x���z [�U��]"
    ''' <summary>
    ''' �����x���z [�U��]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngFurikomi As Long
    ''' <summary>
    ''' �����x���z [�U��]
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����x���z [�U��]</returns>
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

#Region "�����x���z [���E]"
    ''' <summary>
    ''' �����x���z [���E]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngSousai As Long
    ''' <summary>
    ''' �����x���z [���E]
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����x���z [���E]</returns>
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

#Region "�����x�����v"
    ''' <summary>
    ''' �����x�����v
    ''' </summary>
    ''' <remarks></remarks>
    Private lngTougetuShriGoukei As Long
    ''' <summary>
    ''' �����x�����v
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����x�����v</returns>
    ''' <remarks></remarks>
    <TableMap("tougetu_shri_goukei")> _
    Public Property TougetuShriGoukei() As Long
        Get
            Return lngTougetuShriGoukei
        End Get
        Set(ByVal value As Long)
            lngTougetuShriGoukei = value
        End Set
    End Property
#End Region

#Region "�����d����"
    ''' <summary>
    ''' �����d����
    ''' </summary>
    ''' <remarks></remarks>
    Private lngTougetuSiireNado As Long
    ''' <summary>
    ''' �����d����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����d����</returns>
    ''' <remarks></remarks>
    <TableMap("tougetu_siire_nado")> _
    Public Property TougetuSiireNado() As Long
        Get
            Return lngTougetuSiireNado
        End Get
        Set(ByVal value As Long)
            lngTougetuSiireNado = value
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