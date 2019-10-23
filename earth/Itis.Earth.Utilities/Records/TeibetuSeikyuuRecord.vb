<TableClassMap("t_teibetu_seikyuu")> _
Public Class TeibetuSeikyuuRecord

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
    <TableMap("kbn", IsKey:=True, IsUpdate:=False, IsInsert:=True, DeleteKey:=True, SqlType:=SqlDbType.Char, SqlLength:=1)> _
    Public Overridable Property Kbn() As String
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
    <TableMap("hosyousyo_no", IsKey:=True, IsUpdate:=False, IsInsert:=True, DeleteKey:=True, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Overridable Property HosyousyoNo() As String
        Get
            Return strHosyousyoNo
        End Get
        Set(ByVal value As String)
            strHosyousyoNo = value
        End Set
    End Property
#End Region

#Region "���޺���"
    ''' <summary>
    ''' ���޺���
    ''' </summary>
    ''' <remarks></remarks>
    Private strBunruiCd As String
    ''' <summary>
    ''' ���޺���
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���޺���</returns>
    ''' <remarks></remarks>
    <TableMap("bunrui_cd", IsKey:=True, IsUpdate:=True, IsInsert:=True, DeleteKey:=True, SqlType:=SqlDbType.VarChar, SqlLength:=3)> _
    Public Overridable Property BunruiCd() As String
        Get
            Return strBunruiCd
        End Get
        Set(ByVal value As String)
            strBunruiCd = value
        End Set
    End Property
#End Region

#Region "��ʕ\��NO"
    ''' <summary>
    ''' ��ʕ\��NO
    ''' </summary>
    ''' <remarks></remarks>
    Private intGamenHyoujiNo As Integer
    ''' <summary>
    ''' ��ʕ\��NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> ��ʕ\��NO</returns>
    ''' <remarks></remarks>
    <TableMap("gamen_hyouji_no", IsKey:=True, IsUpdate:=False, IsInsert:=True, DeleteKey:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property GamenHyoujiNo() As Integer
        Get
            Return intGamenHyoujiNo
        End Get
        Set(ByVal value As Integer)
            intGamenHyoujiNo = value
        End Set
    End Property
#End Region

#Region "���i����"
    ''' <summary>
    ''' ���i����
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyouhinCd As String
    ''' <summary>
    ''' ���i����
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���i����</returns>
    ''' <remarks></remarks>
    <TableMap("syouhin_cd", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.VarChar, SqlLength:=8)> _
    Public Overridable Property SyouhinCd() As String
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
    Public Overridable Property SyouhinMei() As String
        Get
            Return strSyouhinMei
        End Get
        Set(ByVal value As String)
            strSyouhinMei = value
        End Set
    End Property
#End Region

#Region "������z"
    ''' <summary>
    ''' ������z
    ''' </summary>
    ''' <remarks></remarks>
    Private intUriGaku As Integer
    ''' <summary>
    ''' ������z
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������z</returns>
    ''' <remarks></remarks>
    <TableMap("uri_gaku", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property UriGaku() As Integer
        Get
            Return intUriGaku
        End Get
        Set(ByVal value As Integer)
            intUriGaku = value
        End Set
    End Property
#End Region

#Region "�d�����z"
    ''' <summary>
    ''' �d�����z
    ''' </summary>
    ''' <remarks></remarks>
    Private intSiireGaku As Integer
    ''' <summary>
    ''' �d�����z
    ''' </summary>
    ''' <value></value>
    ''' <returns> �d�����z</returns>
    ''' <remarks></remarks>
    <TableMap("siire_gaku", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property SiireGaku() As Integer
        Get
            Return intSiireGaku
        End Get
        Set(ByVal value As Integer)
            intSiireGaku = value
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
    <TableMap("zei_kbn", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.VarChar, SqlLength:=1)> _
    Public Overridable Property ZeiKbn() As String
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
    ''' <returns> �ŗ�</returns>
    ''' <remarks></remarks>
    <TableMap("zeiritu")> _
    Public Overridable Property Zeiritu() As Decimal
        Get
            Return decZeiritu
        End Get
        Set(ByVal value As Decimal)
            decZeiritu = value
        End Set
    End Property
#End Region

#Region "�������Ŋz"
    ''' <summary>
    ''' �������Ŋz
    ''' </summary>
    ''' <remarks></remarks>
    Private intUriageSyouhiZeiGaku As Integer = Integer.MinValue
    ''' <summary>
    ''' �������Ŋz
    ''' </summary>
    ''' <value></value>
    ''' <returns>�������Ŋz</returns>
    ''' <remarks></remarks>
    <TableMap("syouhizei_gaku", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property UriageSyouhiZeiGaku() As Integer
        Get
            Dim intTmpUriGaku As Integer = IIf(UriGaku = Integer.MinValue, 0, UriGaku) '������z
            Dim decTmpZeiritu As Decimal = IIf(Zeiritu = Decimal.MinValue, 0, Zeiritu) '�ŗ�

            '����Ŋz
            If intUriageSyouhiZeiGaku = Integer.MinValue Then 'NULL�̏ꍇ�A������z�Ɛŗ����犷�Z
                intUriageSyouhiZeiGaku = Fix(intTmpUriGaku * decTmpZeiritu) '����Ŋz = ������z * �ŗ�
            End If
            Return intUriageSyouhiZeiGaku
        End Get
        Set(ByVal value As Integer)
            intUriageSyouhiZeiGaku = value
        End Set
    End Property
#End Region

#Region "�d������Ŋz"
    ''' <summary>
    ''' �d������Ŋz
    ''' </summary>
    ''' <remarks></remarks>
    Private intSiireSyouhiZeiGaku As Integer = Integer.MinValue
    ''' <summary>
    ''' �d������Ŋz
    ''' </summary>
    ''' <value></value>
    ''' <returns>�d������Ŋz</returns>
    ''' <remarks></remarks>
    <TableMap("siire_syouhizei_gaku", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.Int, SqlLength:=1)> _
    Public Overridable Property SiireSyouhiZeiGaku() As Integer
        Get
            Dim intTmpSiireGaku As Integer = IIf(SiireGaku = Integer.MinValue, 0, SiireGaku) '�d�����z
            Dim decTmpZeiritu As Decimal = IIf(Zeiritu = Decimal.MinValue, 0, Zeiritu) '�ŗ�

            '����Ŋz
            If intSiireSyouhiZeiGaku = Integer.MinValue Then 'NULL�̏ꍇ�A������z�Ɛŗ����犷�Z
                intSiireSyouhiZeiGaku = Fix(intTmpSiireGaku * decTmpZeiritu) '����Ŋz = �d�����z * �ŗ�
            End If
            Return intSiireSyouhiZeiGaku
        End Get
        Set(ByVal value As Integer)
            intSiireSyouhiZeiGaku = value
        End Set
    End Property
#End Region

#Region "���������s��"
    ''' <summary>
    ''' ���������s��
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSeikyuusyoHakDate As DateTime
    ''' <summary>
    ''' ���������s��
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���������s��</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuusyo_hak_date", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property SeikyuusyoHakDate() As DateTime
        Get
            Return dateSeikyuusyoHakDate
        End Get
        Set(ByVal value As DateTime)
            dateSeikyuusyoHakDate = value
        End Set
    End Property
#End Region

#Region "����N����"
    ''' <summary>
    ''' ����N����
    ''' </summary>
    ''' <remarks></remarks>
    Private dateUriDate As DateTime = DateTime.MinValue
    ''' <summary>
    ''' ����N����
    ''' </summary>
    ''' <value></value>
    ''' <returns> ����N����</returns>
    ''' <remarks></remarks>
    <TableMap("uri_date", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property UriDate() As DateTime
        Get
            Return dateUriDate
        End Get
        Set(ByVal value As DateTime)
            dateUriDate = value
        End Set
    End Property
#End Region

#Region "�`�[����N����"
    ''' <summary>
    ''' �`�[����N����
    ''' </summary>
    ''' <remarks></remarks>
    Private dateDenpyouUriDate As DateTime = DateTime.MinValue
    ''' <summary>
    ''' �`�[����N����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �`�[����N����</returns>
    ''' <remarks></remarks>
    <TableMap("denpyou_uri_date", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property DenpyouUriDate() As DateTime
        Get
            Return dateDenpyouUriDate
        End Get
        Set(ByVal value As DateTime)
            dateDenpyouUriDate = value
        End Set
    End Property
#End Region

#Region "�`�[�d���N����"
    ''' <summary>
    ''' �`�[�d���N����
    ''' </summary>
    ''' <remarks></remarks>
    Private dateDenpyouSiireDate As DateTime = DateTime.MinValue
    ''' <summary>
    ''' �`�[�d���N����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �`�[�d���N����</returns>
    ''' <remarks></remarks>
    <TableMap("denpyou_siire_date", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.DateTime, SqlLength:=1)> _
    Public Overridable Property DenpyouSiireDate() As DateTime
        Get
            Return dateDenpyouSiireDate
        End Get
        Set(ByVal value As DateTime)
            dateDenpyouSiireDate = value
        End Set
    End Property
#End Region

#Region "�����L��"
    ''' <summary>
    ''' �����L��
    ''' </summary>
    ''' <remarks></remarks>
    Private intSeikyuuUmu As Integer
    ''' <summary>
    ''' �����L��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����L��</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_umu", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property SeikyuuUmu() As Integer
        Get
            Return intSeikyuuUmu
        End Get
        Set(ByVal value As Integer)
            intSeikyuuUmu = value
        End Set
    End Property
#End Region

#Region "�m��敪"
    ''' <summary>
    ''' �m��敪
    ''' </summary>
    ''' <remarks></remarks>
    Private intKakuteiKbn As Integer
    ''' <summary>
    ''' �m��敪
    ''' </summary>
    ''' <value></value>
    ''' <returns> �m��敪</returns>
    ''' <remarks></remarks>
    <TableMap("kakutei_kbn", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property KakuteiKbn() As Integer
        Get
            Return intKakuteiKbn
        End Get
        Set(ByVal value As Integer)
            intKakuteiKbn = value
        End Set
    End Property
#End Region

#Region "����v��FLG"
    ''' <summary>
    ''' ����v��FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intUriKeijyouFlg As Integer
    ''' <summary>
    ''' ����v��FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns> ����v��FLG</returns>
    ''' <remarks></remarks>
    <TableMap("uri_keijyou_flg", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property UriKeijyouFlg() As Integer
        Get
            Return intUriKeijyouFlg
        End Get
        Set(ByVal value As Integer)
            intUriKeijyouFlg = value
        End Set
    End Property
#End Region

#Region "����v���"
    ''' <summary>
    ''' ����v���
    ''' </summary>
    ''' <remarks></remarks>
    Private dateUriKeijyouDate As DateTime
    ''' <summary>
    ''' ����v���
    ''' </summary>
    ''' <value></value>
    ''' <returns> ����v���</returns>
    ''' <remarks></remarks>
    <TableMap("uri_keijyou_date", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property UriKeijyouDate() As DateTime
        Get
            Return dateUriKeijyouDate
        End Get
        Set(ByVal value As DateTime)
            dateUriKeijyouDate = value
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
    <TableMap("bikou", IsKey:=False, IsUpdate:=False, IsInsert:=False, SqlType:=SqlDbType.VarChar, SqlLength:=40)> _
    Public Overridable Property Bikou() As String
        Get
            Return strBikou
        End Get
        Set(ByVal value As String)
            strBikou = value
        End Set
    End Property
#End Region

#Region "�H���X�������z"
    ''' <summary>
    ''' �H���X�������z
    ''' </summary>
    ''' <remarks></remarks>
    Private intKoumutenSeikyuuGaku As Integer
    ''' <summary>
    ''' �H���X�������z
    ''' </summary>
    ''' <value></value>
    ''' <returns> �H���X�������z</returns>
    ''' <remarks></remarks>
    <TableMap("koumuten_seikyuu_gaku", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property KoumutenSeikyuuGaku() As Integer
        Get
            Return intKoumutenSeikyuuGaku
        End Get
        Set(ByVal value As Integer)
            intKoumutenSeikyuuGaku = value
        End Set
    End Property
#End Region

#Region "���������z"
    ''' <summary>
    ''' ���������z
    ''' </summary>
    ''' <remarks></remarks>
    Private intHattyuusyoGaku As Integer
    ''' <summary>
    ''' ���������z
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���������z</returns>
    ''' <remarks></remarks>
    <TableMap("hattyuusyo_gaku", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property HattyuusyoGaku() As Integer
        Get
            If intHattyuusyoGaku = 0 Then
                Return Integer.MinValue
            End If

            Return intHattyuusyoGaku
        End Get
        Set(ByVal value As Integer)
            intHattyuusyoGaku = value
        End Set
    End Property
#End Region

#Region "�������m�F��"
    ''' <summary>
    ''' �������m�F��
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHattyuusyoKakuninDate As DateTime
    ''' <summary>
    ''' �������m�F��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �������m�F��</returns>
    ''' <remarks></remarks>
    <TableMap("hattyuusyo_kakunin_date", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property HattyuusyoKakuninDate() As DateTime
        Get
            Return dateHattyuusyoKakuninDate
        End Get
        Set(ByVal value As DateTime)
            dateHattyuusyoKakuninDate = value
        End Set
    End Property
#End Region

#Region "�ꊇ����FLG"
    ''' <summary>
    ''' �ꊇ����FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intIkkatuNyuukinFlg As Integer
    ''' <summary>
    ''' �ꊇ����FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ꊇ����FLG</returns>
    ''' <remarks></remarks>
    <TableMap("ikkatu_nyuukin_flg", IsKey:=False, IsUpdate:=False, IsInsert:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property IkkatuNyuukinFlg() As Integer
        Get
            Return intIkkatuNyuukinFlg
        End Get
        Set(ByVal value As Integer)
            intIkkatuNyuukinFlg = value
        End Set
    End Property
#End Region

#Region "�������Ϗ��쐬��"
    ''' <summary>
    ''' �������Ϗ��쐬��
    ''' </summary>
    ''' <remarks></remarks>
    Private dateTysMitsyoSakuseiDate As DateTime
    ''' <summary>
    ''' �������Ϗ��쐬��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �������Ϗ��쐬��</returns>
    ''' <remarks></remarks>
    <TableMap("tys_mitsyo_sakusei_date", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property TysMitsyoSakuseiDate() As DateTime
        Get
            Return dateTysMitsyoSakuseiDate
        End Get
        Set(ByVal value As DateTime)
            dateTysMitsyoSakuseiDate = value
        End Set
    End Property
#End Region

#Region "�������m��FLG"
    ''' <summary>
    ''' �������m��FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intHattyuusyoKakuteiFlg As Integer
    ''' <summary>
    ''' �������m��FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns> �������m��FLG</returns>
    ''' <remarks></remarks>
    <TableMap("hattyuusyo_kakutei_flg", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overridable Property HattyuusyoKakuteiFlg() As Integer
        Get
            Return intHattyuusyoKakuteiFlg
        End Get
        Set(ByVal value As Integer)
            intHattyuusyoKakuteiFlg = value
        End Set
    End Property
#End Region

#Region "������/�d����"

#Region "������R�[�h"
    ''' <summary>
    ''' ������R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiCd As String = Nothing
    ''' <summary>
    ''' ������R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������R�[�h</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_cd", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.VarChar, SqlLength:=5)> _
    Public Overridable Property SeikyuuSakiCd() As String
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
    Private strSeikyuuSakiBrc As String = Nothing
    ''' <summary>
    ''' ������}��
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������}��</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_brc", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
    Public Overridable Property SeikyuuSakiBrc() As String
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
    Private strSeikyuuSakiKbn As String = Nothing
    ''' <summary>
    ''' ������敪
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������敪</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_kbn", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.Char, SqlLength:=1)> _
    Public Overridable Property SeikyuuSakiKbn() As String
        Get
            Return strSeikyuuSakiKbn
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiKbn = value
        End Set
    End Property
#End Region

#Region "������ЃR�[�h"
    ''' <summary>
    ''' ������ЃR�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaCd As String = Nothing
    ''' <summary>
    ''' ������ЃR�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������ЃR�[�h</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kaisya_cd", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.VarChar, SqlLength:=5)> _
    Public Overridable Property TysKaisyaCd() As String
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
    Private strTysKaisyaJigyousyoCd As String = Nothing
    ''' <summary>
    ''' ������Ў��Ə��R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������Ў��Ə��R�[�h</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kaisya_jigyousyo_cd", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
    Public Overridable Property TysKaisyaJigyousyoCd() As String
        Get
            Return strTysKaisyaJigyousyoCd
        End Get
        Set(ByVal value As String)
            strTysKaisyaJigyousyoCd = value
        End Set
    End Property
#End Region

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
    <TableMap("add_login_user_id", IsKey:=False, IsUpdate:=False, IsInsert:=True, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Overridable Property AddLoginUserId() As String
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
    <TableMap("add_datetime", IsKey:=False, IsUpdate:=False, IsInsert:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property AddDatetime() As DateTime
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
    <TableMap("upd_login_user_id", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Overridable Property UpdLoginUserId() As String
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
    <TableMap("upd_datetime", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overridable Property UpdDatetime() As DateTime
        Get
            Return dateUpdDatetime
        End Get
        Set(ByVal value As DateTime)
            dateUpdDatetime = value
        End Set
    End Property
#End Region

#Region "�ō�������z"
    ''' <summary>
    ''' �ō�������z
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ō�������z</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ZeikomiUriGaku() As Integer
        Get
            Dim intTmpUriGaku As Integer = IIf(UriGaku = Integer.MinValue, 0, UriGaku)
            Dim decTmpZeiritu As Decimal = IIf(Zeiritu = Decimal.MinValue, 0, Zeiritu)
            If UriageSyouhiZeiGaku = Integer.MinValue Then '����Ŋz=NULL�̏ꍇ�A�ŗ�����Z�o
                intTmpUriGaku = intTmpUriGaku + Fix(intTmpUriGaku * decTmpZeiritu) '�ō�������z = ������z + (������z * ����ŗ�)
            Else
                intTmpUriGaku = intTmpUriGaku + UriageSyouhiZeiGaku     '�ō�������z = ������z + ����Ŋz
            End If
            Return intTmpUriGaku
        End Get
    End Property
#End Region

#Region "�ō��d�����z"
    ''' <summary>
    ''' �ō��d�����z
    ''' </summary>
    ''' <value></value>
    ''' <returns>�ō��d�����z</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ZeikomiSiireGaku() As Integer
        Get
            Dim intTmpSirGaku As Integer = IIf(SiireGaku = Integer.MinValue, 0, SiireGaku)
            Dim decTmpZeiritu As Decimal = IIf(Zeiritu = Decimal.MinValue, 0, Zeiritu)
            If SiireSyouhiZeiGaku = Integer.MinValue Then
                '�ŗ�����Z�o
                intTmpSirGaku = intTmpSirGaku + Fix(intTmpSirGaku * decTmpZeiritu) '�ō��d�����z = �d�����z + (�d�����z * ����ŗ�)
            Else

            End If
            '�ŗ�����Z�o
            intTmpSirGaku = intTmpSirGaku + SiireSyouhiZeiGaku      '�ō��d�����z = �d�����z + ����Ŋz
            Return intTmpSirGaku
        End Get
    End Property
#End Region

#Region "���������z�ύX��FLG"
    ''' <summary>
    ''' ���������z�ύX��FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private blnSyoudakuHenkouKahi As Boolean
    ''' <summary>
    ''' ���������z�ύX��FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns>���������z�ύX��FLG</returns>
    ''' <remarks></remarks>
    Public Property SyoudakuHenkouKahi() As Boolean
        Get
            Return blnSyoudakuHenkouKahi
        End Get
        Set(ByVal value As Boolean)
            blnSyoudakuHenkouKahi = value
        End Set
    End Property
#End Region

#Region "�H���X�������z�ύX��FLG"
    ''' <summary>
    ''' �H���X�������z�ύX��FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private blnKoumutenHenkouKahi As Boolean
    ''' <summary>
    ''' �H���X�������z�ύX��FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns>�H���X�������z�ύX��FLG</returns>
    ''' <remarks></remarks>
    Public Property KoumutenHenkouKahi() As Boolean
        Get
            Return blnKoumutenHenkouKahi
        End Get
        Set(ByVal value As Boolean)
            blnKoumutenHenkouKahi = value
        End Set
    End Property
#End Region

#Region "���������z�ύX��FLG"
    ''' <summary>
    ''' ���������z�ύX��FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private blnJituseikyuuHenkouKahi As Boolean
    ''' <summary>
    ''' ���������z�ύX��FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns>���������z�ύX��FLG</returns>
    ''' <remarks></remarks>
    Public Property JituseikyuuHenkouKahi() As Boolean
        Get
            Return blnJituseikyuuHenkouKahi
        End Get
        Set(ByVal value As Boolean)
            blnJituseikyuuHenkouKahi = value
        End Set
    End Property
#End Region

#Region "�e�탁�\�b�h"
#Region "���ʑΉ����i�Ή�"
    ''' <summary>
    ''' �@�ʐ����̕ύX�󋵂̔���
    ''' </summary>
    ''' <remarks></remarks>
    Private emKkkJyky As EarthEnum.emTeibetuSeikyuuKkkJyky = EarthEnum.emTeibetuSeikyuuKkkJyky.NONE
    ''' <summary>
    ''' �@�ʐ����̕ύX�󋵂̔���
    ''' </summary>
    ''' <value></value>
    ''' <returns>�@�ʐ����̕ύX�󋵂̔���</returns>
    ''' <remarks></remarks>
    Public Property KkkHenkouCheck() As EarthEnum.emTeibetuSeikyuuKkkJyky
        Get
            Return emKkkJyky
        End Get
        Set(ByVal value As EarthEnum.emTeibetuSeikyuuKkkJyky)
            emKkkJyky = value
        End Set
    End Property

#End Region

#End Region

End Class