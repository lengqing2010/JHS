''' <summary>
''' �n�Ճf�[�^�̃��R�[�h�N���X�ł�
''' �@�ʓ����f�[�^�C����ʗp�̍X�V�f�[�^�\���ł�
''' </summary>
''' <remarks>�n�Ճe�[�u���̑S���R�[�h�J�����ɉ����A�@�ʃf�[�^��ێ����Ă܂�<br/>
'''          ���i�R�[�h�P�̓@�ʐ������R�[�h�FTeibetuSeikyuuRecord<br/>
'''          ���i�R�[�h�Q�̓@�ʐ������R�[�h�FDictionary(Of Integer, TeibetuSeikyuuRecord)<br/>
'''          ���i�R�[�h�R�̓@�ʐ������R�[�h�FDictionary(Of Integer, TeibetuSeikyuuRecord)<br/>
'''          �ǉ��H���̓@�ʐ������R�[�h�@�@�FTeibetuSeikyuuRecord<br/>
'''          ���ǍH���̓@�ʐ������R�[�h�@�@�FTeibetuSeikyuuRecord<br/>
'''          �����񍐏��̓@�ʐ������R�[�h�@�FTeibetuSeikyuuRecord<br/>
'''          �H���񍐏��̓@�ʐ������R�[�h�@�FTeibetuSeikyuuRecord<br/>
'''          �ۏ؏��̓@�ʐ������R�[�h�@�@�@�FTeibetuSeikyuuRecord<br/>
'''          ��񕥖߂̓@�ʐ������R�[�h�@�@�FTeibetuSeikyuuRecord<br/>
'''          ��L�ȊO�̓@�ʐ������R�[�h    �FList(TeibetuSeikyuuRecord)<br/>
'''          �@�ʓ������R�[�h              �FDictionary(Of String, TeibetuNyuukinRecord)<br/>
''' </remarks>
<TableClassMap("t_jiban")> _
Public Class JibanRecordTeibetuNyuukin
    Inherits JibanRecordBase

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
    <TableMap("kbn", IsKey:=True, IsUpdate:=False, SqlType:=SqlDbType.Char, SqlLength:=1)> _
    Public Overrides Property Kbn() As String
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
    <TableMap("hosyousyo_no", IsKey:=True, IsUpdate:=False, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Overrides Property HosyousyoNo() As String
        Get
            Return strHosyousyoNo
        End Get
        Set(ByVal value As String)
            strHosyousyoNo = value
        End Set
    End Property
#End Region

#Region "�ԋ�����FLG"
    ''' <summary>
    ''' �ԋ�����FLG
    ''' </summary>
    ''' <remarks></remarks>
    Private intHenkinSyoriFlg As Integer
    ''' <summary>
    ''' �ԋ�����FLG
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ԋ�����FLG</returns>
    ''' <remarks></remarks>
    <TableMap("henkin_syori_flg", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.Int, SqlLength:=4)> _
    Public Overrides Property HenkinSyoriFlg() As Integer
        Get
            Return intHenkinSyoriFlg
        End Get
        Set(ByVal value As Integer)
            intHenkinSyoriFlg = value
        End Set
    End Property
#End Region

#Region "�ԋ�������"
    ''' <summary>
    ''' �ԋ�������
    ''' </summary>
    ''' <remarks></remarks>
    Private dateHenkinSyoriDate As DateTime
    ''' <summary>
    ''' �ԋ�������
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ԋ�������</returns>
    ''' <remarks></remarks>
    <TableMap("henkin_syori_date", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property HenkinSyoriDate() As DateTime
        Get
            Return dateHenkinSyoriDate
        End Get
        Set(ByVal value As DateTime)
            dateHenkinSyoriDate = value
        End Set
    End Property
#End Region

#Region "�X�V۸޲�հ�ްID"
    ''' <summary>
    ''' �X�V۸޲�հ�ްID
    ''' </summary>
    ''' <remarks></remarks>
    Private strUpdLoginUserId As String
    ''' <summary>
    ''' �X�V۸޲�հ�ްID
    ''' </summary>
    ''' <value></value>
    ''' <returns> �X�V۸޲�հ�ްID</returns>
    ''' <remarks></remarks>
    <TableMap("upd_login_user_id", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Overrides Property UpdLoginUserId() As String
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
    ''' <remarks>�r��������s���ׁA�������̍X�V���t��ݒ肵�Ă�������<br/>
    '''          �X�V���̓��t�̓V�X�e�����t���ݒ肳��܂�</remarks>
    <TableMap("upd_datetime", IsKey:=True, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property UpdDatetime() As DateTime
        Get
            Return dateUpdDatetime
        End Get
        Set(ByVal value As DateTime)
            dateUpdDatetime = value
        End Set
    End Property
#End Region

#Region "�X�V��"
    ''' <summary>
    ''' �X�V��
    ''' </summary>
    ''' <remarks></remarks>
    Private strKousinsya As String
    ''' <summary>
    ''' �X�V��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �X�V��</returns>
    ''' <remarks></remarks>
    <TableMap("kousinsya", IsKey:=False, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Overrides Property Kousinsya() As String
        Get
            Return strKousinsya
        End Get
        Set(ByVal value As String)
            strKousinsya = value
        End Set
    End Property
#End Region

End Class