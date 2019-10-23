<TableClassMap("t_teibetu_nyuukin")> _
Public Class TeibetuNyuukinRecord

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
    Public Property Kbn() As String
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
    Public Property HosyousyoNo() As String
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
    <TableMap("bunrui_cd", IsKey:=True, IsUpdate:=False, IsInsert:=True, DeleteKey:=True, SqlType:=SqlDbType.VarChar, SqlLength:=3)> _
    Public Property BunruiCd() As String
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
    ''' <returns> ���޺���</returns>
    ''' <remarks></remarks>
    <TableMap("gamen_hyouji_no", IsKey:=True, IsUpdate:=False, IsInsert:=True, DeleteKey:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property GamenHyoujiNo() As Integer
        Get
            Return intGamenHyoujiNo
        End Get
        Set(ByVal value As Integer)
            intGamenHyoujiNo = value
        End Set
    End Property
#End Region

#Region "�������z"
    ''' <summary>
    ''' �������z
    ''' </summary>
    ''' <remarks>�ō��������z - �ō��ԋ����z</remarks>
    Private intNyuukinGaku As Integer
    ''' <summary>
    ''' �������z
    ''' </summary>
    ''' <value></value>
    ''' <returns> �������z</returns>
    ''' <remarks></remarks>
    <TableMap("nyuukin_gaku")> _
    Public Property NyuukinGaku() As Integer
        Get
            Return intNyuukinGaku
        End Get
        Set(ByVal value As Integer)
            intNyuukinGaku = value
        End Set
    End Property
#End Region

#Region "�ō��������z"
    ''' <summary>
    ''' �ō��������z
    ''' </summary>
    ''' <remarks>�ō��������z</remarks>
    Private intZeikomiNyuukinGaku As Integer
    ''' <summary>
    ''' �ō��������z
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ō��������z</returns>
    ''' <remarks></remarks>
    <TableMap("zeikomi_nyuukin_gaku", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property ZeikomiNyuukinGaku() As Integer
        Get
            Return intZeikomiNyuukinGaku
        End Get
        Set(ByVal value As Integer)
            intZeikomiNyuukinGaku = value
        End Set
    End Property
#End Region

#Region "�ō��ԋ����z"
    ''' <summary>
    ''' �ō��ԋ����z
    ''' </summary>
    ''' <remarks>�ō��ԋ����z</remarks>
    Private intZeikomiHenkinGaku As Integer
    ''' <summary>
    ''' �ō��ԋ����z
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ō��ԋ����z</returns>
    ''' <remarks></remarks>
    <TableMap("zeikomi_henkin_gaku", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Property ZeikomiHenkinGaku() As Integer
        Get
            Return intZeikomiHenkinGaku
        End Get
        Set(ByVal value As Integer)
            intZeikomiHenkinGaku = value
        End Set
    End Property
#End Region

#Region "�ŏI������"
    ''' <summary>
    ''' �ŏI������
    ''' </summary>
    ''' <remarks></remarks>
    Private dateSaisyuuNyuukinDate As DateTime
    ''' <summary>
    ''' �ŏI������
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ŏI������</returns>
    ''' <remarks></remarks>
    <TableMap("saisyuu_nyuukin_date", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Property SaisyuuNyuukinDate() As DateTime
        Get
            Return dateSaisyuuNyuukinDate
        End Get
        Set(ByVal value As DateTime)
            dateSaisyuuNyuukinDate = value
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
    <TableMap("add_login_user_id", IsKey:=False, IsUpdate:=False, IsInsert:=True, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
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
    <TableMap("add_datetime", IsKey:=False, IsUpdate:=False, IsInsert:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
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
    <TableMap("upd_login_user_id", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
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
    <TableMap("upd_datetime", IsKey:=False, IsUpdate:=True, IsInsert:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
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