''' <summary>
''' �n�Ճf�[�^�̔r���`�F�b�N�p���R�[�h�N���X�ł�
''' </summary>
''' <remarks>Kye���ڂƍX�V���[�U�[�A�X�V������ێ����A���[���̍X�V�`�F�b�N���s���܂�</remarks>
<TableClassMap("t_jiban")> _
Public Class JibanHaitaRecord

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
    <TableMap("kbn", IsKey:=True, IsUpdate:=True, SqlType:=SqlDbType.Char, SqlLength:=1)> _
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
    <TableMap("hosyousyo_no", IsKey:=True, IsUpdate:=True, SqlType:=SqlDbType.VarChar, SqlLength:=10)> _
    Public Property HosyousyoNo() As String
        Get
            Return strHosyousyoNo
        End Get
        Set(ByVal value As String)
            strHosyousyoNo = value
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
    ''' <remarks>�r��������s���ׁA�������̍X�V���t��ݒ肵�Ă�������<br/>
    '''          �X�V���̓��t�̓V�X�e�����t���ݒ肳��܂�</remarks>
    <TableMap("upd_datetime", IsKey:=True, IsUpdate:=True, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
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