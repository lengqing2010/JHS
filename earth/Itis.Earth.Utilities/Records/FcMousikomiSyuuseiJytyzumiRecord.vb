''' <summary>
''' FC�\���C���󒍍σ��R�[�h�N���X/FC�\���C�����
''' </summary>
''' <remarks>FC�\���f�[�^�̏C�����Ɏg�p���܂�(�󒍍ώ��p)</remarks>
<TableClassMap("FcMousikomiIF")> _
Public Class FcMousikomiSyuuseiJytyzumiRecord
    Inherits FcMousikomiRecord

    'EMAB��Q�Ή����i�[����
    Private ReadOnly ASSEMBLY_CLASSNAME As String = Me.GetType.FullName

#Region "�\��NO"
    ''' <summary>
    ''' �\��NO
    ''' </summary>
    ''' <remarks></remarks>
    Private lngMousikomiNo As Long = Long.MinValue
    ''' <summary>
    ''' �\��NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> �\��NO</returns>
    ''' <remarks></remarks>
    <TableMap("mousikomi_no", IsKey:=True, IsUpdate:=False, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.BigInt, SqlLength:=8)> _
    Public Overrides Property MousikomiNo() As Long
        Get
            Return lngMousikomiNo
        End Get
        Set(ByVal value As Long)
            lngMousikomiNo = value
        End Set
    End Property
#End Region

#Region "�����A����_�S����"
    ''' <summary>
    ''' �����A����_�S����
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysRenrakusakiTantouMei As String = String.Empty
    ''' <summary>
    ''' �����A����_�S����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����A����_�S����</returns>
    ''' <remarks></remarks>
    <TableMap("tys_renrakusaki_tantou_mei", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=20)> _
    Public Overrides Property TysRenrakusakiTantouMei() As String
        Get
            Return strTysRenrakusakiTantouMei
        End Get
        Set(ByVal value As String)
            strTysRenrakusakiTantouMei = value
        End Set
    End Property
#End Region

#Region "�����(���̑��⑫)"
    ''' <summary>
    ''' �����(���̑��⑫)
    ''' </summary>
    ''' <remarks></remarks>
    Private strTtsySonotaHosoku As String = String.Empty
    ''' <summary>
    ''' �����(���̑��⑫)
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����(���̑��⑫)</returns>
    ''' <remarks></remarks>
    <TableMap("tatiaisya_sonota_hosoku", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overrides Property TtsySonotaHosoku() As String
        Get
            Return strTtsySonotaHosoku
        End Get
        Set(ByVal value As String)
            strTtsySonotaHosoku = value
        End Set
    End Property
#End Region

#Region "�v���ӏ��"
    ''' <summary>
    ''' �v���ӏ��
    ''' </summary>
    ''' <remarks></remarks>
    Private strYouTyuuiJouhou As String = String.Empty
    ''' <summary>
    ''' �v���ӏ��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �v���ӏ��</returns>
    ''' <remarks></remarks>
    <TableMap("you_tyuui_jouhou", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overrides Property YouTyuuiJouhou() As String
        Get
            Return strYouTyuuiJouhou
        End Get
        Set(ByVal value As String)
            strYouTyuuiJouhou = value
        End Set
    End Property
#End Region

#Region "������ВS����"
    ''' <summary>
    ''' ������ВS����
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysKaisyaTantousya As String = String.Empty
    ''' <summary>
    ''' ������ВS����
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������ВS����</returns>
    ''' <remarks></remarks>
    <TableMap("tys_kaisya_cd_tantousya", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=256)> _
    Public Overrides Property TysKaisyaTantousya() As String
        Get
            Return strTysKaisyaTantousya
        End Get
        Set(ByVal value As String)
            strTysKaisyaTantousya = value
        End Set
    End Property
#End Region

#Region "�X�V��"
    ''' <summary>
    ''' �X�V��
    ''' </summary>
    ''' <remarks></remarks>
    Private strKousinsya As String = String.Empty
    ''' <summary>
    ''' �X�V��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �X�V��</returns>
    ''' <remarks></remarks>
    <TableMap("kousinsya", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Overrides Property Kousinsya() As String
        Get
            Return strKousinsya
        End Get
        Set(ByVal value As String)
            strKousinsya = value
        End Set
    End Property
#End Region

#Region "�X�V����"
    ''' <summary>
    ''' �X�V����
    ''' </summary>
    ''' <remarks></remarks>
    Private dateUpdDatetime As DateTime = DateTime.MinValue
    ''' <summary>
    ''' �X�V����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �X�V����</returns>
    ''' <remarks></remarks>
    <TableMap("upd_datetime", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.DateTime, SqlLength:=16)> _
    Public Overrides Property UpdDatetime() As DateTime
        Get
            Return dateUpdDatetime
        End Get
        Set(ByVal value As DateTime)
            dateUpdDatetime = value
        End Set
    End Property
#End Region

#Region "�X�V���O�C�����[�U�[ID"
    ''' <summary>
    ''' �X�V���O�C�����[�U�[ID
    ''' </summary>
    ''' <remarks></remarks>
    Private strUpdLoginUserId As String = String.Empty
    ''' <summary>
    ''' �X�V���O�C�����[�U�[ID
    ''' </summary>
    ''' <value></value>
    ''' <returns> �X�V���O�C�����[�U�[ID</returns>
    ''' <remarks></remarks>
    <TableMap("upd_login_user_id", IsKey:=False, IsUpdate:=True, IsInsert:=False, DeleteKey:=False, SqlType:=SqlDbType.VarChar, SqlLength:=30)> _
    Public Overrides Property UpdLoginUserId() As String
        Get
            Return strUpdLoginUserId
        End Get
        Set(ByVal value As String)
            strUpdLoginUserId = value
        End Set
    End Property
#End Region

End Class
