Public Class Syouhin23Record

#Region "���i�R�[�h"
    ''' <summary>
    ''' ���i�R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouhinCd As String
    ''' <summary>
    ''' ���i�R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���i�R�[�h</returns>
    ''' <remarks></remarks>
    <TableMap("syouhin_cd")> _
    Public Property SyouhinCd() As String
        Get
            Return strSyouhinCd
        End Get
        Set(ByVal value As String)
            strSyouhinCd = value
        End Set
    End Property
#End Region

#Region "���i��"
    ''' <summary>
    ''' ���i��
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouhinMei As String
    ''' <summary>
    ''' ���i��
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���i��</returns>
    ''' <remarks></remarks>
    <TableMap("syouhin_mei")> _
    Public Property SyouhinMei() As String
        Get
            Return strSyouhinMei
        End Get
        Set(ByVal value As String)
            strSyouhinMei = value
        End Set
    End Property
#End Region

#Region "�W�����i"
    ''' <summary>
    ''' �W�����i
    ''' </summary>
    ''' <remarks></remarks>
    Private intHyoujunKkk As Integer
    ''' <summary>
    ''' �W�����i
    ''' </summary>
    ''' <value></value>
    ''' <returns> �W�����i</returns>
    ''' <remarks></remarks>
    <TableMap("hyoujun_kkk")> _
    Public Property HyoujunKkk() As Integer
        Get
            Return intHyoujunKkk
        End Get
        Set(ByVal value As Integer)
            intHyoujunKkk = value
        End Set
    End Property
#End Region

#Region "�q�ɃR�[�h"
    ''' <summary>
    ''' �q�ɃR�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private strSoukoCd As String
    ''' <summary>
    ''' �q�ɃR�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> �q�ɃR�[�h</returns>
    ''' <remarks></remarks>
    <TableMap("souko_cd")> _
    Public Property SoukoCd() As String
        Get
            Return strSoukoCd
        End Get
        Set(ByVal value As String)
            strSoukoCd = value
        End Set
    End Property
#End Region

#Region "�ŋ敪"
    ''' <summary>
    ''' �ŋ敪
    ''' </summary>
    ''' <remarks></remarks>
    Private strZeiKbn As String
    ''' <summary>
    ''' �ŋ敪
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ŋ敪</returns>
    ''' <remarks></remarks>
    <TableMap("zei_kbn")> _
    Public Property ZeiKbn() As String
        Get
            Return strZeiKbn
        End Get
        Set(ByVal value As String)
            strZeiKbn = value
        End Set
    End Property
#End Region

#Region "�d�����i"
    ''' <summary>
    ''' �d�����i
    ''' </summary>
    ''' <remarks></remarks>
    Private intSiireKkk As Integer
    ''' <summary>
    ''' �d�����i
    ''' </summary>
    ''' <value></value>
    ''' <returns> �d�����i</returns>
    ''' <remarks></remarks>
    <TableMap("siire_kkk")> _
    Public Property SiireKkk() As Integer
        Get
            Return intSiireKkk
        End Get
        Set(ByVal value As Integer)
            intSiireKkk = value
        End Set
    End Property
#End Region

#Region "�ŗ�"
    ''' <summary>
    ''' �ŗ�
    ''' </summary>
    ''' <remarks></remarks>
    Private decZeiritu As Decimal
    ''' <summary>
    ''' �ŗ�
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ŗ�</returns>
    ''' <remarks></remarks>
    <TableMap("zeiritu")> _
    Public Property Zeiritu() As Decimal
        Get
            Return decZeiritu
        End Get
        Set(ByVal value As Decimal)
            decZeiritu = value
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

#Region "������R�[�h(��{)"
    ''' <summary>
    ''' ������R�[�h(��{)
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiCd As String
    ''' <summary>
    ''' ������R�[�h(��{)
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������R�[�h(��{)</returns>
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

#Region "������}��(��{)"
    ''' <summary>
    ''' ������}��(��{)
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiBrc As String
    ''' <summary>
    ''' ������}��(��{)
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������}��(��{)</returns>
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

#Region "������敪(��{)"
    ''' <summary>
    ''' ������敪(��{)
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiKbn As String
    ''' <summary>
    ''' ������敪(��{)
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������敪(��{)</returns>
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

#Region "������^�C�v(����or������)"
    ''' <summary>
    ''' ������^�C�v(����or������)
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiType As String
    ''' <summary>
    ''' ������^�C�v(����or������)
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������^�C�v(����or������)</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_kbn")> _
    Public Property SeikyuuSakiType() As String
        Get
            If KameitenCd <> String.Empty AndAlso _
               SeikyuuSakiCd <> String.Empty AndAlso _
               KameitenCd = SeikyuuSakiCd Then
                Return EarthConst.SEIKYU_TYOKUSETU
            Else
                Return EarthConst.SEIKYU_TASETU
            End If
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiType = value
        End Set
    End Property
#End Region

#Region "�ۏؗL��"
    ''' <summary>
    ''' �ۏؗL��
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyouUmu As Integer
    ''' <summary>
    ''' �ۏؗL��
    ''' </summary>
    ''' <value></value>
    ''' <returns>�ۏؗL��</returns>
    ''' <remarks></remarks>
    <TableMap("hosyou_umu")> _
    Public Property HosyouUmu() As Integer
        Get
            Return intHosyouUmu
        End Get
        Set(ByVal value As Integer)
            intHosyouUmu = value
        End Set
    End Property
#End Region

End Class