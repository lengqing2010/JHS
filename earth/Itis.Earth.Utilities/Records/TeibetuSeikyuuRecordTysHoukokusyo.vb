''' <summary>
''' �@�ʐ����e�[�u��/�X�V�p���R�[�h�N���X[�����񍐏��Ĕ��s]
''' �����L�v���p�e�B���ڂōX�V�Ώۂ�ݒ�ύX����
''' </summary>
''' <remarks>
''' </remarks>
<TableClassMap("t_teibetu_seikyuu")> _
Public Class TeibetuSeikyuuRecordTysHoukokusyo
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

#Region "�m��敪"
    ''' <summary>
    ''' �m��敪
    ''' </summary>
    ''' <remarks></remarks>
    Private intKakuteiKbn As Integer = Integer.MinValue
    ''' <summary>
    ''' �m��敪
    ''' </summary>
    ''' <value></value>
    ''' <returns> �m��敪</returns>
    ''' <remarks></remarks>
    <TableMap("kakutei_kbn", IsKey:=False, IsUpdate:=False, IsInsert:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
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
    <TableMap("uri_keijyou_date", IsKey:=False, IsUpdate:=False, IsInsert:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property UriKeijyouDate() As DateTime
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
    <TableMap("bikou", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.VarChar, SqlLength:=40)> _
    Public Overrides Property Bikou() As String
        Get
            Return strBikou
        End Get
        Set(ByVal value As String)
            strBikou = value
        End Set
    End Property
#End Region

#Region "���������z"
    ''' <summary>
    ''' ���������z
    ''' </summary>
    ''' <remarks></remarks>
    Private intHattyuusyoGaku As Integer = Integer.MinValue
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

End Class