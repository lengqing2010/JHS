Public Class LoginUserInfo

#Region "���O�C�����[�U�[ID"
    ''' <summary>
    ''' ���O�C�����[�U�[ID
    ''' </summary>
    ''' <remarks></remarks>
    Private strLoginUserId As String
    ''' <summary>
    ''' ���O�C�����[�U�[ID
    ''' </summary>
    ''' <value></value>
    ''' <returns>���O�C�����[�U�[ID</returns>
    ''' <remarks></remarks>
    <TableMap("login_user_id")> _
    Public Property LoginUserId() As String
        Get
            Return strLoginUserId
        End Get
        Set(ByVal value As String)
            strLoginUserId = value
        End Set
    End Property
#End Region

#Region "�A�J�E���gNO"
    ''' <summary>
    ''' �A�J�E���gNO
    ''' </summary>
    ''' <remarks></remarks>
    Private intAccountNo As Integer
    ''' <summary>
    ''' �A�J�E���gNO
    ''' </summary>
    ''' <value></value>
    ''' <returns>�A�J�E���gNO</returns>
    ''' <remarks></remarks>
    <TableMap("account_no")> _
    Public Property AccountNo() As Integer
        Get
            Return intAccountNo
        End Get
        Set(ByVal value As Integer)
            intAccountNo = value
        End Set
    End Property
#End Region

#Region "�A�J�E���g"
    ''' <summary>
    ''' �A�J�E���g
    ''' </summary>
    ''' <remarks></remarks>
    Private strAccount As String
    ''' <summary>
    ''' �A�J�E���g
    ''' </summary>
    ''' <value></value>
    ''' <returns>�A�J�E���g</returns>
    ''' <remarks></remarks>
    <TableMap("account")> _
    Public Property Account() As String
        Get
            Return strAccount
        End Get
        Set(ByVal value As String)
            strAccount = value
        End Set
    End Property
#End Region

#Region "����"
    ''' <summary>
    ''' ����
    ''' </summary>
    ''' <remarks></remarks>
    Private strName As String
    ''' <summary>
    ''' ����
    ''' </summary>
    ''' <value></value>
    ''' <returns>����</returns>
    ''' <remarks></remarks>
    <TableMap("simei")> _
    Public Property Name() As String
        Get
            Return strName
        End Get
        Set(ByVal value As String)
            strName = value
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
    ''' <returns>���l</returns>
    ''' <remarks></remarks>
    <TableMap("bikou")> _
    Public Property Bikou() As String
        Get
            Return strBikou
        End Get
        Set(ByVal value As String)
            strBikou = value
        End Set
    End Property
#End Region

#Region "�˗��Ɩ�����"
    ''' <summary>
    ''' �˗��Ɩ�����
    ''' </summary>
    ''' <remarks></remarks>
    Private intIraiGyoumuKengen As Integer
    ''' <summary>
    ''' �˗��Ɩ�����
    ''' </summary>
    ''' <value></value>
    ''' <returns>�˗��Ɩ�����</returns>
    ''' <remarks></remarks>
    <TableMap("irai_gyoumu_kengen")> _
    Public Property IraiGyoumuKengen() As Integer
        Get
            Return intIraiGyoumuKengen
        End Get
        Set(ByVal value As Integer)
            intIraiGyoumuKengen = value
        End Set
    End Property
#End Region

#Region "���ʋƖ�����"
    ''' <summary>
    ''' ���ʋƖ�����
    ''' </summary>
    ''' <remarks></remarks>
    Private intKekkaGyoumuKengen As Integer
    ''' <summary>
    ''' ���ʋƖ�����
    ''' </summary>
    ''' <value></value>
    ''' <returns>���ʋƖ�����</returns>
    ''' <remarks></remarks>
    <TableMap("kekka_gyoumu_kengen")> _
    Public Property KekkaGyoumuKengen() As Integer
        Get
            Return intKekkaGyoumuKengen
        End Get
        Set(ByVal value As Integer)
            intKekkaGyoumuKengen = value
        End Set
    End Property
#End Region

#Region "�ۏ؋Ɩ�����"
    ''' <summary>
    ''' �ۏ؋Ɩ�����
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyouGyoumuKengen As Integer
    ''' <summary>
    ''' �ۏ؋Ɩ�����
    ''' </summary>
    ''' <value></value>
    ''' <returns>�ۏ؋Ɩ�����</returns>
    ''' <remarks></remarks>
    <TableMap("hosyou_gyoumu_kengen")> _
    Public Property HosyouGyoumuKengen() As Integer
        Get
            Return intHosyouGyoumuKengen
        End Get
        Set(ByVal value As Integer)
            intHosyouGyoumuKengen = value
        End Set
    End Property
#End Region

#Region "�񍐏��Ɩ�����"
    ''' <summary>
    ''' �񍐏��Ɩ�����
    ''' </summary>
    ''' <remarks></remarks>
    Private intHoukokusyoGyoumuKengen As Integer
    ''' <summary>
    ''' �񍐏��Ɩ�����
    ''' </summary>
    ''' <value></value>
    ''' <returns>�񍐏��Ɩ�����</returns>
    ''' <remarks></remarks>
    <TableMap("hkks_gyoumu_kengen")> _
    Public Property HoukokusyoGyoumuKengen() As Integer
        Get
            Return intHoukokusyoGyoumuKengen
        End Get
        Set(ByVal value As Integer)
            intHoukokusyoGyoumuKengen = value
        End Set
    End Property
#End Region

#Region "�H���Ɩ�����"
    ''' <summary>
    ''' �H���Ɩ�����
    ''' </summary>
    ''' <remarks></remarks>
    Private intKoujiGyoumuKengen As Integer
    ''' <summary>
    ''' �H���Ɩ�����
    ''' </summary>
    ''' <value></value>
    ''' <returns>�H���Ɩ�����</returns>
    ''' <remarks></remarks>
    <TableMap("koj_gyoumu_kengen")> _
    Public Property KoujiGyoumuKengen() As Integer
        Get
            Return intKoujiGyoumuKengen
        End Get
        Set(ByVal value As Integer)
            intKoujiGyoumuKengen = value
        End Set
    End Property
#End Region

#Region "�o���Ɩ�����"
    ''' <summary>
    ''' �o���Ɩ�����
    ''' </summary>
    ''' <remarks></remarks>
    Private intKeiriGyoumuKengen As Integer
    ''' <summary>
    ''' �o���Ɩ�����
    ''' </summary>
    ''' <value></value>
    ''' <returns>�o���Ɩ�����</returns>
    ''' <remarks></remarks>
    <TableMap("keiri_gyoumu_kengen")> _
    Public Property KeiriGyoumuKengen() As Integer
        Get
            Return intKeiriGyoumuKengen
        End Get
        Set(ByVal value As Integer)
            intKeiriGyoumuKengen = value
        End Set
    End Property
#End Region

#Region "��̓}�X�^�Ǘ�����"
    ''' <summary>
    ''' ��̓}�X�^�Ǘ�����
    ''' </summary>
    ''' <remarks></remarks>
    Private intKaisekiMasterKanriKengen As Integer
    ''' <summary>
    ''' ��̓}�X�^�Ǘ�����
    ''' </summary>
    ''' <value></value>
    ''' <returns>��̓}�X�^�Ǘ�����</returns>
    ''' <remarks></remarks>
    <TableMap("kaiseki_master_kanri_kengen")> _
    Public Property KaisekiMasterKanriKengen() As Integer
        Get
            Return intKaisekiMasterKanriKengen
        End Get
        Set(ByVal value As Integer)
            intKaisekiMasterKanriKengen = value
        End Set
    End Property
#End Region

#Region "�c�ƃ}�X�^�Ǘ�����"
    ''' <summary>
    ''' �c�ƃ}�X�^�Ǘ�����
    ''' </summary>
    ''' <remarks></remarks>
    Private intEigyouMasterKanriKengen As Integer
    ''' <summary>
    ''' �c�ƃ}�X�^�Ǘ�����
    ''' </summary>
    ''' <value></value>
    ''' <returns>�c�ƃ}�X�^�Ǘ�����</returns>
    ''' <remarks></remarks>
    <TableMap("eigyou_master_kanri_kengen")> _
    Public Property EigyouMasterKanriKengen() As Integer
        Get
            Return intEigyouMasterKanriKengen
        End Get
        Set(ByVal value As Integer)
            intEigyouMasterKanriKengen = value
        End Set
    End Property
#End Region

#Region "���i�}�X�^�Ǘ�����"
    ''' <summary>
    ''' ���i�}�X�^�Ǘ�����
    ''' </summary>
    ''' <remarks></remarks>
    Private intKakakuMasterKanriKengen As Integer
    ''' <summary>
    ''' ���i�}�X�^�Ǘ�����
    ''' </summary>
    ''' <value></value>
    ''' <returns>���i�}�X�^�Ǘ�����</returns>
    ''' <remarks></remarks>
    <TableMap("kkk_master_kanri_kengen")> _
    Public Property KakakuMasterKanriKengen() As Integer
        Get
            Return intKakakuMasterKanriKengen
        End Get
        Set(ByVal value As Integer)
            intKakakuMasterKanriKengen = value
        End Set
    End Property
#End Region

#Region "�̑����㌠��"
    ''' <summary>
    ''' �̑����㌠��
    ''' </summary>
    ''' <remarks></remarks>
    Private intHansokuUriageKengen As Integer
    ''' <summary>
    ''' �̑����㌠��
    ''' </summary>
    ''' <value></value>
    ''' <returns>�̑����㌠��</returns>
    ''' <remarks></remarks>
    <TableMap("hansoku_uri_kengen")> _
    Public Property HansokuUriageKengen() As Integer
        Get
            Return intHansokuUriageKengen
        End Get
        Set(ByVal value As Integer)
            intHansokuUriageKengen = value
        End Set
    End Property
#End Region

#Region "�f�[�^�j������"
    ''' <summary>
    ''' �f�[�^�j������
    ''' </summary>
    ''' <remarks></remarks>
    Private intDataHakiKengen As Integer
    ''' <summary>
    ''' �f�[�^�j������
    ''' </summary>
    ''' <value></value>
    ''' <returns>�f�[�^�j������</returns>
    ''' <remarks></remarks>
    <TableMap("data_haki_kengen")> _
    Public Property DataHakiKengen() As Integer
        Get
            Return intDataHakiKengen
        End Get
        Set(ByVal value As Integer)
            intDataHakiKengen = value
        End Set
    End Property
#End Region

#Region "�V�X�e���Ǘ��Ҍ���"
    ''' <summary>
    ''' �V�X�e���Ǘ��Ҍ���
    ''' </summary>
    ''' <remarks></remarks>
    Private intSystemKanrisyaKengen As Integer
    ''' <summary>
    ''' �V�X�e���Ǘ��Ҍ���
    ''' </summary>
    ''' <value></value>
    ''' <returns>�V�X�e���Ǘ��Ҍ���</returns>
    ''' <remarks></remarks>
    <TableMap("system_kanrisya_kengen")> _
    Public Property SystemKanrisyaKengen() As Integer
        Get
            Return intSystemKanrisyaKengen
        End Get
        Set(ByVal value As Integer)
            intSystemKanrisyaKengen = value
        End Set
    End Property
#End Region

#Region "�V�K���͌���"
    ''' <summary>
    ''' �V�K���͌���
    ''' </summary>
    ''' <remarks></remarks>
    Private intSinkiNyuuryokuKengen As Integer
    ''' <summary>
    ''' �V�K���͌���
    ''' </summary>
    ''' <value></value>
    ''' <returns>�V�K���͌���</returns>
    ''' <remarks></remarks>
    <TableMap("sinki_nyuuryoku_kengen")> _
    Public Property SinkiNyuuryokuKengen() As Integer
        Get
            Return intSinkiNyuuryokuKengen
        End Get
        Set(ByVal value As Integer)
            intSinkiNyuuryokuKengen = value
        End Set
    End Property
#End Region

#Region "�������Ǘ�����"
    ''' <summary>
    ''' �������Ǘ�����
    ''' </summary>
    ''' <remarks></remarks>
    Private intHattyuusyoKanriKengen As Integer
    ''' <summary>
    ''' �������Ǘ�����
    ''' </summary>
    ''' <value></value>
    ''' <returns>�������Ǘ�����</returns>
    ''' <remarks></remarks>
    <TableMap("hattyuusyo_kanri_kengen")> _
    Public Property HattyuusyoKanriKengen() As Integer
        Get
            Return intHattyuusyoKanriKengen
        End Get
        Set(ByVal value As Integer)
            intHattyuusyoKanriKengen = value
        End Set
    End Property
#End Region


#Region "������(Department)"
    ''' <summary>
    ''' ������(Department)
    ''' </summary>
    ''' <remarks></remarks>
    Private strDepartment As String
    ''' <summary>
    ''' ������(Department)
    ''' </summary>
    ''' <value></value>
    ''' <returns>������(Department)</returns>
    ''' <remarks></remarks>
    <TableMap("Department")> _
    Public Property Department() As String
        Get
            Return strDepartment
        End Get
        Set(ByVal value As String)
            strDepartment = value
        End Set
    End Property
#End Region

End Class
