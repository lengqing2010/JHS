Public Class SeikyuuSakiInfoRecord

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

#Region "�����於"
    ''' <summary>
    ''' �����於
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiMei As String
    ''' <summary>
    ''' �����於
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����於</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_mei")> _
    Public Property SeikyuuSakiMei() As String
        Get
            Return strSeikyuuSakiMei
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiMei = value
        End Set
    End Property
#End Region

#Region "������J�i"
    ''' <summary>
    ''' ������J�i
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiKana As String
    ''' <summary>
    ''' ������J�i
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������J�i</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_kana")> _
    Public Property SeikyuuSakiKana() As String
        Get
            Return strSeikyuuSakiKana
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiKana = value
        End Set
    End Property
#End Region

#Region "���������t��Z��1"
    ''' <summary>
    ''' ���������t��Z��1
    ''' </summary>
    ''' <remarks></remarks>
    Private strSkysySoufuJyuusyo1 As String
    ''' <summary>
    ''' ���������t��Z��1
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���������t��Z��1</returns>
    ''' <remarks></remarks>
    <TableMap("skysy_soufu_jyuusyo1")> _
    Public Property SkysySoufuJyuusyo1() As String
        Get
            Return strSkysySoufuJyuusyo1
        End Get
        Set(ByVal value As String)
            strSkysySoufuJyuusyo1 = value
        End Set
    End Property
#End Region

#Region "���������t��Z��2"
    ''' <summary>
    ''' ���������t��Z��2
    ''' </summary>
    ''' <remarks></remarks>
    Private strSkysySoufuJyuusyo2 As String
    ''' <summary>
    ''' ���������t��Z��2
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���������t��Z��2</returns>
    ''' <remarks></remarks>
    <TableMap("skysy_soufu_jyuusyo2")> _
    Public Property SkysySoufuJyuusyo2() As String
        Get
            Return strSkysySoufuJyuusyo2
        End Get
        Set(ByVal value As String)
            strSkysySoufuJyuusyo2 = value
        End Set
    End Property
#End Region

#Region "���������t��X�֔ԍ�"
    ''' <summary>
    ''' ���������t��X�֔ԍ�
    ''' </summary>
    ''' <remarks></remarks>
    Private strSkysySoufuYuubinNo As String
    ''' <summary>
    ''' ���������t��X�֔ԍ�
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���������t��X�֔ԍ�</returns>
    ''' <remarks></remarks>
    <TableMap("skysy_soufu_yuubin_no")> _
    Public Property SkysySoufuYuubinNo() As String
        Get
            Return strSkysySoufuYuubinNo
        End Get
        Set(ByVal value As String)
            strSkysySoufuYuubinNo = value
        End Set
    End Property
#End Region

#Region "���������t��d�b�ԍ�"
    ''' <summary>
    ''' ���������t��d�b�ԍ�
    ''' </summary>
    ''' <remarks></remarks>
    Private strSkysySoufuTelNo As String
    ''' <summary>
    ''' ���������t��d�b�ԍ�
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���������t��d�b�ԍ�</returns>
    ''' <remarks></remarks>
    <TableMap("skysy_soufu_tel_no")> _
    Public Property SkysySoufuTelNo() As String
        Get
            Return strSkysySoufuTelNo
        End Get
        Set(ByVal value As String)
            strSkysySoufuTelNo = value
        End Set
    End Property
#End Region

#Region "���������t��FAX�ԍ�"
    ''' <summary>
    ''' ���������t��FAX�ԍ�
    ''' </summary>
    ''' <remarks></remarks>
    Private strSkysySoufuFaxNo As String
    ''' <summary>
    ''' ���������t��FAX�ԍ�
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���������t��FAX�ԍ�</returns>
    ''' <remarks></remarks>
    <TableMap("skysy_soufu_fax_no")> _
    Public Property SkysySoufuFaxNo() As String
        Get
            Return strSkysySoufuFaxNo
        End Get
        Set(ByVal value As String)
            strSkysySoufuFaxNo = value
        End Set
    End Property
#End Region

End Class