''' <summary>
''' ����f�[�^�e�[�u���̃��R�[�h�N���X�ł�
''' </summary>
''' <remarks></remarks>
<TableClassMap("t_uriage_data")> _
Public Class UriageDataRecord

#Region "�`�[���j�[�NNO"
    ''' <summary>
    ''' �`�[���j�[�NNO
    ''' </summary>
    ''' <remarks></remarks>
    Private intDenUnqNo As Integer
    ''' <summary>
    ''' �`�[���j�[�NNO
    ''' </summary>
    ''' <value></value>
    ''' <returns> �`�[���j�[�NNO</returns>
    ''' <remarks></remarks>
    <TableMap("denpyou_unique_no", IsKey:=True, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property DenUnqNo() As Integer
        Get
            Return intDenUnqNo
        End Get
        Set(ByVal value As Integer)
            intDenUnqNo = value
        End Set
    End Property
#End Region

#Region "�`�[NO"
    ''' <summary>
    ''' �`�[NO
    ''' </summary>
    ''' <remarks></remarks>
    Private strDenNo As String
    ''' <summary>
    ''' �`�[NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> �`�[NO</returns>
    ''' <remarks></remarks>
    <TableMap("denpyou_no", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Char, SqlLength:=5)> _
    Public Property DenNo() As String
        Get
            Return strDenNo
        End Get
        Set(ByVal value As String)
            strDenNo = value
        End Set
    End Property
#End Region

#Region "�`�[���"
    ''' <summary>
    ''' �`�[���
    ''' </summary>
    ''' <remarks></remarks>
    Private strDenSyubetu As String
    ''' <summary>
    ''' �`�[���
    ''' </summary>
    ''' <value></value>
    ''' <returns> �`�[���</returns>
    ''' <remarks></remarks>
    <TableMap("denpyou_syubetu", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
    Public Property DenSyubetu() As String
        Get
            Return strDenSyubetu
        End Get
        Set(ByVal value As String)
            strDenSyubetu = value
        End Set
    End Property
#End Region

#Region "������`�[���j�[�NNO"
    ''' <summary>
    ''' ������`�[���j�[�NNO
    ''' </summary>
    ''' <remarks></remarks>
    Private strToriMotoDenUnqNo As Integer
    ''' <summary>
    ''' ������`�[���j�[�NNO
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������`�[���j�[�NNO</returns>
    ''' <remarks></remarks>
    <TableMap("torikesi_moto_denpyou_unique_no", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property ToriMotoDenUnqNo() As Integer
        Get
            Return strToriMotoDenUnqNo
        End Get
        Set(ByVal value As Integer)
            strToriMotoDenUnqNo = value
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
    <TableMap("kbn", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Char, SqlLength:=1)> _
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
    <TableMap("bangou", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Property Bangou() As String
        Get
            Return strBangou
        End Get
        Set(ByVal value As String)
            strBangou = value
        End Set
    End Property
#End Region

#Region "�R�t���R�[�h"
    ''' <summary>
    ''' �R�t���R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private strHimodukeCd As String
    ''' <summary>
    ''' �R�t���R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> �R�t���R�[�h</returns>
    ''' <remarks></remarks>
    <TableMap("himoduke_cd", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Property HimodukeCd() As String
        Get
            Return strHimodukeCd
        End Get
        Set(ByVal value As String)
            strHimodukeCd = value
        End Set
    End Property
#End Region

#Region "�R�t�����e�[�u�����"
    ''' <summary>
    ''' �R�t�����e�[�u�����
    ''' </summary>
    ''' <remarks></remarks>
    Private strHimodukeMotoTblSyubetu As String
    ''' <summary>
    ''' �R�t�����e�[�u�����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �R�t�����e�[�u�����</returns>
    ''' <remarks></remarks>
    <TableMap("himoduke_table_type", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property HimodukeMotoTblSyubetu() As Integer
        Get
            Return strHimodukeMotoTblSyubetu
        End Get
        Set(ByVal value As Integer)
            strHimodukeMotoTblSyubetu = value
        End Set
    End Property
#End Region

#Region "����N����"
    ''' <summary>
    ''' ����N����
    ''' </summary>
    ''' <remarks></remarks>
    Private dateUriDate As DateTime
    ''' <summary>
    ''' ����N����
    ''' </summary>
    ''' <value></value>
    ''' <returns> ����N����</returns>
    ''' <remarks></remarks>
    <TableMap("uri_date", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property UriDate() As DateTime
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
    Private dateDenUriDate As DateTime
    ''' <summary>
    ''' �`�[����N����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �`�[����N����</returns>
    ''' <remarks></remarks>
    <TableMap("denpyou_uri_date", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property DenUriDate() As DateTime
        Get
            Return dateDenUriDate
        End Get
        Set(ByVal value As DateTime)
            dateDenUriDate = value
        End Set
    End Property
#End Region

#Region "�����N����"
    ''' <summary>
    ''' �����N����
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSeikyuuDate As DateTime
    ''' <summary>
    ''' �����N����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����N����</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_date", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property SeikyuuDate() As DateTime
        Get
            Return dateSeikyuuDate
        End Get
        Set(ByVal value As DateTime)
            dateSeikyuuDate = value
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
    <TableMap("seikyuu_saki_cd", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=5)> _
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
    <TableMap("seikyuu_saki_brc", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=2)> _
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
    <TableMap("seikyuu_saki_kbn", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Char, SqlLength:=1)> _
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
    <TableMap("seikyuu_saki_mei", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=80)> _
    Public Property SeikyuuSakiMei() As String
        Get
            Return strSeikyuuSakiMei
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiMei = value
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
    <TableMap("syouhin_cd", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=8)> _
    Public Property SyouhinCd() As String
        Get
            Return strSyouhinCd
        End Get
        Set(ByVal value As String)
            strSyouhinCd = value
        End Set
    End Property
#End Region

#Region "�i��"
    ''' <summary>
    ''' �i��
    ''' </summary>
    ''' <remarks></remarks>
    Private strHinmei As String
    ''' <summary>
    ''' �i��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �i��</returns>
    ''' <remarks></remarks>
    <TableMap("hinmei", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
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
    Private intSuu As Integer
    ''' <summary>
    ''' ����
    ''' </summary>
    ''' <value></value>
    ''' <returns> ����</returns>
    ''' <remarks></remarks>
    <TableMap("suu", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
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
    Private strTani As String
    ''' <summary>
    ''' �P��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �P��</returns>
    ''' <remarks></remarks>
    <TableMap("tani", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=4)> _
    Public Property Tani() As String
        Get
            Return strTani
        End Get
        Set(ByVal value As String)
            strTani = value
        End Set
    End Property
#End Region

#Region "�P��"
    ''' <summary>
    ''' �P��
    ''' </summary>
    ''' <remarks></remarks>
    Private intTanka As Integer
    ''' <summary>
    ''' �P��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �P��</returns>
    ''' <remarks></remarks>
    <TableMap("tanka", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property Tanka() As Integer
        Get
            Return intTanka
        End Get
        Set(ByVal value As Integer)
            intTanka = value
        End Set
    End Property
#End Region

#Region "������z"
    ''' <summary>
    ''' ������z
    ''' </summary>
    ''' <remarks></remarks>
    Private lngUriGaku As Long
    ''' <summary>
    ''' ������z
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������z</returns>
    ''' <remarks></remarks>
    <TableMap("uri_gaku", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.BigInt, SqlLength:=8)> _
    Public Property UriGaku() As Long
        Get
            Return lngUriGaku
        End Get
        Set(ByVal value As Long)
            lngUriGaku = value
        End Set
    End Property
#End Region

#Region "�O�Ŋz"
    ''' <summary>
    ''' �O�Ŋz
    ''' </summary>
    ''' <remarks></remarks>
    Private intSotozeiGaku As Integer
    ''' <summary>
    ''' �O�Ŋz
    ''' </summary>
    ''' <value></value>
    ''' <returns> �O�Ŋz</returns>
    ''' <remarks></remarks>
    <TableMap("sotozei_gaku", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property SotozeiGaku() As Integer
        Get
            Return intSotozeiGaku
        End Get
        Set(ByVal value As Integer)
            intSotozeiGaku = value
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
    <TableMap("zei_kbn", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=1)> _
    Public Property ZeiKbn() As String
        Get
            Return strZeiKbn
        End Get
        Set(ByVal value As String)
            strZeiKbn = value
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
    <TableMap("add_login_user_id", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
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
    <TableMap("add_datetime", IsKey:=False, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
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
    <TableMap("upd_login_user_id", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
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
    <TableMap("upd_datetime", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
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
