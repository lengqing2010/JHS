''' <summary>
''' ���i�R�[�h1�̎����ݒ背�R�[�h
''' </summary>
''' <remarks></remarks>
Public Class Syouhin1AutoSetRecord

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
    ''' <returns>���i�R�[�h</returns>
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
    Private strSyouhinNm As String
    ''' <summary>
    ''' ���i��
    ''' </summary>
    ''' <value></value>
    ''' <returns>���i��</returns>
    ''' <remarks></remarks>
    <TableMap("syouhin_mei")> _
    Public Property SyouhinNm() As String
        Get
            Return strSyouhinNm
        End Get
        Set(ByVal value As String)
            strSyouhinNm = value
        End Set
    End Property
#End Region

#Region "�����T�v"
    ''' <summary>
    ''' �����T�v
    ''' </summary>
    ''' <remarks></remarks>
    Private intTyousaGaiyou As Integer
    ''' <summary>
    ''' �����T�v(���w�莞��9��ݒ肵�Ă�������)
    ''' </summary>
    ''' <value></value>
    ''' <returns>�����T�v</returns>
    ''' <remarks></remarks>
    Public Property TyousaGaiyou() As Integer
        Get
            Return intTyousaGaiyou
        End Get
        Set(ByVal value As Integer)
            intTyousaGaiyou = value
        End Set
    End Property
#End Region

#Region "�Ŕ����i"
    ''' <summary>
    ''' �Ŕ����i
    ''' </summary>
    ''' <remarks></remarks>
    Private intZeinuki As Integer
    ''' <summary>
    ''' �Ŕ����i
    ''' </summary>
    ''' <value></value>
    ''' <returns>�Ŕ����i</returns>
    ''' <remarks></remarks>
    Public Property Zeinuki() As Integer
        Get
            Return intZeinuki
        End Get
        Set(ByVal value As Integer)
            intZeinuki = value
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
    ''' <returns>�ŋ敪</returns>
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
    ''' <returns>�ŗ�</returns>
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

#Region "����Łi����j"
    ''' <summary>
    ''' ����Łi����j
    ''' </summary>
    ''' <remarks></remarks>
    Private decTaxUri As Decimal
    ''' <summary>
    ''' ����Łi����j
    ''' </summary>
    ''' <value></value>
    ''' <returns>����Łi����j</returns>
    ''' <remarks></remarks>
    Public Property TaxUri() As Decimal
        Get
            Return decTaxUri
        End Get
        Set(ByVal value As Decimal)
            decTaxUri = value
        End Set
    End Property
#End Region

#Region "���i�ݒ�ꏊ"
    ''' <summary>
    ''' ���i�ݒ�ꏊ
    ''' </summary>
    ''' <remarks></remarks>
    Private intKakakuSettei As Integer
    ''' <summary>
    ''' ���i�ݒ�ꏊ
    ''' </summary>
    ''' <value></value>
    ''' <returns>���i�ݒ�ꏊ</returns>
    ''' <remarks>���i�ݒ�ꏊ(0:���ݒ�,1:�����XM,2:���iM)</remarks>
    Public Property KakakuSettei() As Integer
        Get
            Return intKakakuSettei
        End Get
        Set(ByVal value As Integer)
            intKakakuSettei = value
        End Set
    End Property
#End Region

#Region "�H���X�������z"
    ''' <summary>
    ''' �H���X�������z
    ''' </summary>
    ''' <remarks></remarks>
    Private decKoumutenGaku As Decimal
    ''' <summary>
    ''' �H���X�������z
    ''' </summary>
    ''' <value></value>
    ''' <returns>�H���X�������z</returns>
    ''' <remarks></remarks>
    Public Property KoumutenGaku() As Decimal
        Get
            Return decKoumutenGaku
        End Get
        Set(ByVal value As Decimal)
            decKoumutenGaku = value
        End Set
    End Property
#End Region

#Region "���������z"
    ''' <summary>
    ''' ���������z
    ''' </summary>
    ''' <remarks></remarks>
    Private decJituGaku As Decimal
    ''' <summary>
    ''' ���������z
    ''' </summary>
    ''' <value></value>
    ''' <returns>���������z</returns>
    ''' <remarks></remarks>
    Public Property JituGaku() As Decimal
        Get
            Return decJituGaku
        End Get
        Set(ByVal value As Decimal)
            decJituGaku = value
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
    ''' <returns>�q�ɃR�[�h</returns>
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

#Region "�W�����i"
    ''' <summary>
    ''' �W�����i
    ''' </summary>
    ''' <remarks></remarks>
    Private decHyoujunKakaku As Integer
    ''' <summary>
    ''' �W�����i
    ''' </summary>
    ''' <value></value>
    ''' <returns>�W�����i</returns>
    ''' <remarks></remarks>
    <TableMap("hyoujun_kkk")> _
    Public Property HyoujunKakaku() As Integer
        Get
            Return decHyoujunKakaku
        End Get
        Set(ByVal value As Integer)
            decHyoujunKakaku = value
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

#Region "�H���X�������z�ύXFLG"
    ''' <summary>
    ''' �H���X�������z�ύXFLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intKoumutenGakuHenkouFlg As Boolean = True
    ''' <summary>
    ''' �H���X�������z�ύXFLG
    ''' </summary>
    ''' <value></value>
    ''' <returns>�H���X�������z�ύXFLG </returns>
    ''' <remarks></remarks>
    <TableMap("koumuten_seikyuu_gaku_henkou_flg")> _
    Public Property KoumutenGakuHenkouFlg() As Boolean
        Get
            Return intKoumutenGakuHenkouFlg
        End Get
        Set(ByVal value As Boolean)
            intKoumutenGakuHenkouFlg = value
        End Set
    End Property
#End Region

#Region "���������z�ύXFLG"
    ''' <summary>
    ''' ���������z�ύXFLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intJituGakuHenkouFlg As Boolean = True
    ''' <summary>
    ''' ���������z�ύXFLG
    ''' </summary>
    ''' <value></value>
    ''' <returns>���������z�ύXFLG </returns>
    ''' <remarks></remarks>
    <TableMap("jitu_seikyuu_gaku_henkou_flg")> _
    Public Property JituGakuHenkouFlg() As Boolean
        Get
            Return intJituGakuHenkouFlg
        End Get
        Set(ByVal value As Boolean)
            intJituGakuHenkouFlg = value
        End Set
    End Property
#End Region

#Region "���i1�擾�X�e�[�^�X"
    ''' <summary>
    ''' ���i1�擾�X�e�[�^�X
    ''' </summary>
    ''' <remarks></remarks>
    Private intSetSts As Integer = 0
    ''' <summary>
    ''' ���i1�擾�X�e�[�^�X
    ''' </summary>
    ''' <value></value>
    ''' <returns>���i1�擾�X�e�[�^�X</returns>
    ''' <remarks></remarks>
    Public Property SetSts() As Integer
        Get
            Return intSetSts
        End Get
        Set(ByVal value As Integer)
            intSetSts = value
        End Set
    End Property
#End Region

End Class
