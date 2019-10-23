''' <summary>
''' �@�ʐ����e�[�u��/�X�V�p���R�[�h�N���X[���ʑΉ�]
''' �����L�v���p�e�B���ڂōX�V�Ώۂ�ݒ�ύX����
''' </summary>
''' <remarks>
''' </remarks>
<TableClassMap("t_teibetu_seikyuu")> _
Public Class TeibetuSeikyuuRecordTokubetuTaiou
    Inherits TeibetuSeikyuuRecord

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
    <TableMap("bunrui_cd", IsKey:=True, IsUpdate:=False, IsInsert:=True, DeleteKey:=True, SqlType:=SqlDbType.VarChar, SqlLength:=3)> _
    Public Overrides Property BunruiCd() As String
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
    Public Overrides Property GamenHyoujiNo() As Integer
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
    <TableMap("syouhin_cd", IsKey:=False, IsUpdate:=False, IsInsert:=True, SqlType:=SqlDbType.VarChar, SqlLength:=8)> _
    Public Overrides Property SyouhinCd() As String
        Get
            Return strSyouhinCd
        End Get
        Set(ByVal value As String)
            strSyouhinCd = value
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
    Public Overrides Property UriGaku() As Integer
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
    <TableMap("siire_gaku", IsKey:=False, IsUpdate:=False, IsInsert:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SiireGaku() As Integer
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
    <TableMap("zei_kbn", IsKey:=False, IsUpdate:=False, IsInsert:=True, SqlType:=SqlDbType.VarChar, SqlLength:=1)> _
    Public Overrides Property ZeiKbn() As String
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
    Public Overrides Property Zeiritu() As Decimal
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
    Public Overrides Property UriageSyouhiZeiGaku() As Integer
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
    <TableMap("siire_syouhizei_gaku", IsKey:=False, IsUpdate:=False, IsInsert:=False, SqlType:=SqlDbType.Int, SqlLength:=1)> _
    Public Overrides Property SiireSyouhiZeiGaku() As Integer
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
    <TableMap("seikyuusyo_hak_date", IsKey:=False, IsUpdate:=False, IsInsert:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property SeikyuusyoHakDate() As DateTime
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
    <TableMap("uri_date", IsKey:=False, IsUpdate:=False, IsInsert:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property UriDate() As DateTime
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
    <TableMap("denpyou_uri_date", IsKey:=False, IsUpdate:=False, IsInsert:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property DenpyouUriDate() As DateTime
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
    <TableMap("denpyou_siire_date", IsKey:=False, IsUpdate:=False, IsInsert:=True, SqlType:=SqlDbType.DateTime, SqlLength:=1)> _
    Public Overrides Property DenpyouSiireDate() As DateTime
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
    <TableMap("seikyuu_umu", IsKey:=False, IsUpdate:=False, IsInsert:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property SeikyuuUmu() As Integer
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
    <TableMap("kakutei_kbn", IsKey:=False, IsUpdate:=False, IsInsert:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property KakuteiKbn() As Integer
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
    <TableMap("uri_keijyou_flg", IsKey:=False, IsUpdate:=False, IsInsert:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property UriKeijyouFlg() As Integer
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
    <TableMap("uri_keijyou_date", IsKey:=False, IsUpdate:=False, IsInsert:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property UriKeijyouDate() As DateTime
        Get
            Return dateUriKeijyouDate
        End Get
        Set(ByVal value As DateTime)
            dateUriKeijyouDate = value
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
    Public Overrides Property KoumutenSeikyuuGaku() As Integer
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
    <TableMap("hattyuusyo_gaku", IsKey:=False, IsUpdate:=False, IsInsert:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HattyuusyoGaku() As Integer
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
    <TableMap("hattyuusyo_kakunin_date", IsKey:=False, IsUpdate:=False, IsInsert:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property HattyuusyoKakuninDate() As DateTime
        Get
            Return dateHattyuusyoKakuninDate
        End Get
        Set(ByVal value As DateTime)
            dateHattyuusyoKakuninDate = value
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
    <TableMap("tys_mitsyo_sakusei_date", IsKey:=False, IsUpdate:=False, IsInsert:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property TysMitsyoSakuseiDate() As DateTime
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
    <TableMap("hattyuusyo_kakutei_flg", IsKey:=False, IsUpdate:=False, IsInsert:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HattyuusyoKakuteiFlg() As Integer
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
    <TableMap("seikyuu_saki_cd", IsKey:=False, IsUpdate:=False, IsInsert:=False, SqlType:=SqlDbType.VarChar, SqlLength:=5)> _
    Public Overrides Property SeikyuuSakiCd() As String
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
    <TableMap("seikyuu_saki_brc", IsKey:=False, IsUpdate:=False, IsInsert:=False, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
    Public Overrides Property SeikyuuSakiBrc() As String
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
    <TableMap("seikyuu_saki_kbn", IsKey:=False, IsUpdate:=False, IsInsert:=False, SqlType:=SqlDbType.Char, SqlLength:=1)> _
    Public Overrides Property SeikyuuSakiKbn() As String
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
    <TableMap("tys_kaisya_cd", IsKey:=False, IsUpdate:=False, IsInsert:=False, SqlType:=SqlDbType.VarChar, SqlLength:=5)> _
    Public Overrides Property TysKaisyaCd() As String
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
    <TableMap("tys_kaisya_jigyousyo_cd", IsKey:=False, IsUpdate:=False, IsInsert:=False, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
    Public Overrides Property TysKaisyaJigyousyoCd() As String
        Get
            Return strTysKaisyaJigyousyoCd
        End Get
        Set(ByVal value As String)
            strTysKaisyaJigyousyoCd = value
        End Set
    End Property
#End Region

#End Region

End Class