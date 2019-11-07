''' <summary>
''' �ėp�d���f�[�^�e�[�u���̃��R�[�h�N���X�ł�
''' </summary>
''' <remarks></remarks>
<TableClassMap("t_hannyou_siire")> _
Public Class HannyouSiireRecord

#Region "�ėp�d�����j�[�NNO"
    ''' <summary>
    ''' �ėp�d�����j�[�NNO
    ''' </summary>
    ''' <remarks></remarks>
    Private intHanSiireUnqNo As Integer = 0
    ''' <summary>
    ''' �ėp�d�����j�[�NNO
    ''' </summary>
    ''' <value></value>
    ''' <returns>�ėp�d�����j�[�NNO</returns>
    ''' <remarks></remarks>
    <TableMap("han_siire_unique_no", IsKey:=True, IsInsert:=False, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property HanSiireUnqNo() As Integer
        Get
            Return intHanSiireUnqNo
        End Get
        Set(ByVal value As Integer)
            intHanSiireUnqNo = value
        End Set
    End Property
#End Region

#Region "���"
    ''' <summary>
    ''' ���
    ''' </summary>
    ''' <remarks></remarks>
    Private intTorikesi As Integer = Integer.MinValue
    ''' <summary>
    ''' ���
    ''' </summary>
    ''' <value></value>
    ''' <returns>���</returns>
    ''' <remarks></remarks>
    <TableMap("torikesi", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property Torikesi() As Integer
        Get
            Return intTorikesi
        End Get
        Set(ByVal value As Integer)
            intTorikesi = value
        End Set
    End Property
#End Region

#Region "�E�v"
    ''' <summary>
    ''' �E�v
    ''' </summary>
    ''' <remarks></remarks>
    Private strTekiyou As String
    ''' <summary>
    ''' �E�v
    ''' </summary>
    ''' <value></value>
    ''' <returns>�E�v</returns>
    ''' <remarks></remarks>
    <TableMap("tekiyou", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=512)> _
    Public Property Tekiyou() As String
        Get
            Return strTekiyou
        End Get
        Set(ByVal value As String)
            strTekiyou = value
        End Set
    End Property
#End Region

#Region "�d���N����"
    ''' <summary>
    ''' �d���N����
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSiireDate As DateTime
    ''' <summary>
    ''' �d���N����
    ''' </summary>
    ''' <value></value>
    ''' <returns>�d���N����</returns>
    ''' <remarks></remarks>
    <TableMap("siire_date", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property SiireDate() As DateTime
        Get
            Return dateSiireDate
        End Get
        Set(ByVal value As DateTime)
            dateSiireDate = value
        End Set
    End Property
#End Region

#Region "�`�[�d���N����"
    ''' <summary>
    ''' �`�[�d���N����
    ''' </summary>
    ''' <remarks></remarks>
    Private dateDenpyouSiireDate As DateTime
    ''' <summary>
    ''' �`�[�d���N����
    ''' </summary>
    ''' <value></value>
    ''' <returns>�`�[�d���N����</returns>
    ''' <remarks></remarks>
    <TableMap("denpyou_siire_date", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property DenpyouSiireDate() As DateTime
        Get
            Return dateDenpyouSiireDate
        End Get
        Set(ByVal value As DateTime)
            dateDenpyouSiireDate = value
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
    <TableMap("tys_kaisya_cd", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.VarChar, SqlLength:=5)> _
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
    <TableMap("tys_kaisya_jigyousyo_cd", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
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
    <TableMap("kameiten_cd", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.VarChar, SqlLength:=5)> _
    Public Property KameitenCd() As String
        Get
            Return strKameitenCd
        End Get
        Set(ByVal value As String)
            strKameitenCd = value
        End Set
    End Property
#End Region

#Region "�����X��"
    ''' <summary>
    ''' �����X��
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenMei As String
    ''' <summary>
    ''' �����X��
    ''' </summary>
    ''' <value></value>
    ''' <returns>�����X��</returns>
    ''' <remarks></remarks>
    <TableMap("kameiten_mei1")> _
    Public Property KameitenMei() As String
        Get
            Return strKameitenMei
        End Get
        Set(ByVal value As String)
            strKameitenMei = value
        End Set
    End Property
#End Region

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
    <TableMap("syouhin_cd", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.VarChar, SqlLength:=8)> _
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
    Private strHinmei As String
    ''' <summary>
    ''' ���i��
    ''' </summary>
    ''' <value></value>
    ''' <returns>���i��</returns>
    ''' <remarks></remarks>
    <TableMap("syouhin_mei")> _
    Public Property Hinmei() As String
        Get
            Return strHinmei
        End Get
        Set(ByVal value As String)
            strHinmei = value
        End Set
    End Property
#End Region

#Region "����"
    ''' <summary>
    ''' ����
    ''' </summary>
    ''' <remarks></remarks>
    Private intSuu As Integer = Integer.MinValue
    ''' <summary>
    ''' ����
    ''' </summary>
    ''' <value></value>
    ''' <returns> ����</returns>
    ''' <remarks></remarks>
    <TableMap("suu", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property Suu() As Integer
        Get
            Return intSuu
        End Get
        Set(ByVal value As Integer)
            intSuu = value
        End Set
    End Property
#End Region

#Region "�P��"
    ''' <summary>
    ''' �P��
    ''' </summary>
    ''' <remarks></remarks>
    Private intTanka As Integer = Integer.MinValue
    ''' <summary>
    ''' �P��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �P��</returns>
    ''' <remarks></remarks>
    <TableMap("tanka", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property Tanka() As Integer
        Get
            Return intTanka
        End Get
        Set(ByVal value As Integer)
            intTanka = value
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
    <TableMap("zei_kbn", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=1)> _
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
    Private decZeiritu As Decimal = 0
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

#Region "����Ŋz"
    ''' <summary>
    ''' ����Ŋz
    ''' </summary>
    ''' <remarks></remarks>
    Private intSyouhiZeiGaku As Integer = Integer.MinValue
    ''' <summary>
    ''' ����Ŋz
    ''' </summary>
    ''' <value></value>
    ''' <returns>����Ŋz</returns>
    ''' <remarks></remarks>
    <TableMap("syouhizei_gaku", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property SyouhiZeiGaku() As Integer
        Get
            Return intSyouhiZeiGaku
        End Get
        Set(ByVal value As Integer)
            intSyouhiZeiGaku = value
        End Set
    End Property
#End Region

#Region "�d�����z[�P��*����*�ŗ�]"
    ''' <summary>
    ''' �d�����z[�P��*����*�ŗ�]
    ''' </summary>
    ''' <remarks></remarks>
    Private lngSiireGaku As Long = 0
    ''' <summary>
    ''' �d�����z[�P��*����*�ŗ�]
    ''' </summary>
    ''' <value></value>
    ''' <returns>������z[�P��*����*�ŗ�]</returns>
    ''' <remarks></remarks>
    Public Property SiireGaku() As Long
        Get
            Dim tmpUriGaku As Long
            Dim tmpTanka As Long = Long.Parse(Tanka)
            Dim tmpSuu As Long = Long.Parse(Suu)

            If Suu = Integer.MinValue _
                OrElse Tanka = Integer.MinValue Then
                tmpUriGaku = 0
            Else
                tmpUriGaku = tmpSuu * tmpTanka '����*�P��
            End If

            If SyouhiZeiGaku = Integer.MinValue Then
                tmpUriGaku = Fix(tmpUriGaku * (1 + Zeiritu)) '1+�ŗ�
            Else
                tmpUriGaku = Fix(tmpUriGaku + SyouhiZeiGaku) '����Ŋz
            End If

            Return tmpUriGaku
        End Get
        Set(ByVal value As Long)
            lngSiireGaku = value
        End Set
    End Property
#End Region

#Region "�d���v��FLG"
    ''' <summary>
    ''' �d���v��FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intSiireKeijyouFlg As Integer = Integer.MinValue
    ''' <summary>
    ''' �d���v��FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns>�d���v��FLG</returns>
    ''' <remarks></remarks>
    <TableMap("siire_keijyou_flg", IsKey:=False, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property SiireKeijyouFlg() As Integer
        Get
            Return intSiireKeijyouFlg
        End Get
        Set(ByVal value As Integer)
            intSiireKeijyouFlg = value
        End Set
    End Property
#End Region

#Region "�d���v���"
    ''' <summary>
    ''' �d���v���
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSiireKeijyouDate As DateTime = DateTime.MinValue
    ''' <summary>
    ''' �d���v���
    ''' </summary>
    ''' <value></value>
    ''' <returns>�d���v���</returns>
    ''' <remarks></remarks>
    <TableMap("siire_keijyou_date", IsKey:=False, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property SiireKeijyouDate() As DateTime
        Get
            Return dateSiireKeijyouDate
        End Get
        Set(ByVal value As DateTime)
            dateSiireKeijyouDate = value
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
    <TableMap("add_login_user_id", IsKey:=False, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Property AddLoginUserId() As String
        Get
            Return strAddLoginUserId
        End Get
        Set(ByVal value As String)
            strAddLoginUserId = value
        End Set
    End Property
#End Region

#Region "�o�^���O�C�����[�U�[��"
    ''' <summary>
    ''' �o�^���O�C�����[�U�[��
    ''' </summary>
    ''' <remarks></remarks>
    Private strAddLoginUserName As String
    ''' <summary>
    ''' �o�^���O�C�����[�U�[��
    ''' </summary>
    ''' <value></value>
    ''' <returns>�o�^���O�C�����[�U�[��</returns>
    ''' <remarks></remarks>
    <TableMap("add_login_user_name", IsKey:=False, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=128)> _
    Public Property AddLoginUserName() As String
        Get
            Return strAddLoginUserName
        End Get
        Set(ByVal value As String)
            strAddLoginUserName = value
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
    <TableMap("add_datetime", IsKey:=False, IsInsert:=True, IsUpdate:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
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
    <TableMap("upd_login_user_id", IsKey:=False, IsInsert:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Property UpdLoginUserId() As String
        Get
            Return strUpdLoginUserId
        End Get
        Set(ByVal value As String)
            strUpdLoginUserId = value
        End Set
    End Property
#End Region

#Region "�X�V���O�C�����[�U�[��"
    ''' <summary>
    ''' �X�V���O�C�����[�U�[��
    ''' </summary>
    ''' <remarks></remarks>
    Private strUpdLoginUserName As String
    ''' <summary>
    ''' �X�V���O�C�����[�U�[��
    ''' </summary>
    ''' <value></value>
    ''' <returns>�X�V���O�C�����[�U�[��</returns>
    ''' <remarks></remarks>
    <TableMap("upd_login_user_name", IsKey:=False, IsInsert:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=128)> _
    Public Property UpdLoginUserName() As String
        Get
            Return strUpdLoginUserName
        End Get
        Set(ByVal value As String)
            strUpdLoginUserName = value
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
    <TableMap("upd_datetime", IsKey:=False, IsInsert:=False, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property UpdDatetime() As DateTime
        Get
            Return dateUpdDatetime
        End Get
        Set(ByVal value As DateTime)
            dateUpdDatetime = value
        End Set
    End Property
#End Region

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
    <TableMap("kbn", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.Char, SqlLength:=1)> _
    Public Property Kbn() As String
        Get
            Return strKbn
        End Get
        Set(ByVal value As String)
            strKbn = value
        End Set
    End Property
#End Region

#Region "�ԍ�"
    ''' <summary>
    ''' �ԍ�
    ''' </summary>
    ''' <remarks></remarks>
    Private strBangou As String
    ''' <summary>
    ''' �ԍ�
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ԍ�</returns>
    ''' <remarks></remarks>
    <TableMap("bangou", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Property Bangou() As String
        Get
            Return strBangou
        End Get
        Set(ByVal value As String)
            strBangou = value
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
    <TableMap("sesyu_mei", IsKey:=False, IsInsert:=True, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=50)> _
    Public Property SesyuMei() As String
        Get
            Return strSesyuMei
        End Get
        Set(ByVal value As String)
            strSesyuMei = value
        End Set
    End Property
#End Region

End Class