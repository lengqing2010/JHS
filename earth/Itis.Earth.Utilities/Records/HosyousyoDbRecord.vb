Public Class HosyousyoDbRecord

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
    <TableMap("kbn")> _
    Public Property Kbn() As String
        Get
            Return strKbn
        End Get
        Set(ByVal value As String)
            strKbn = value
        End Set
    End Property
#End Region

#Region "�N����From"
    ''' <summary>
    ''' �N����From
    ''' </summary>
    ''' <remarks></remarks>
    Private strDateFrom As String
    ''' <summary>
    ''' �N����From
    ''' </summary>
    ''' <value></value>
    ''' <returns> </returns>
    ''' <remarks>�N����From</remarks>
    <TableMap("date_from")> _
    Public Property DateFrom() As String
        Get
            Return strDateFrom
        End Get
        Set(ByVal value As String)
            strDateFrom = value
        End Set
    End Property
#End Region

#Region "�N����To"
    ''' <summary>
    ''' �N����To
    ''' </summary>
    ''' <remarks></remarks>
    Private strDateTo As String
    ''' <summary>
    ''' �N����To
    ''' </summary>
    ''' <value></value>
    ''' <returns>�N����To </returns>
    ''' <remarks></remarks>
    <TableMap("date_to")> _
    Public Property DateTo() As String
        Get
            Return strDateTo
        End Get
        Set(ByVal value As String)
            strDateTo = value
        End Set
    End Property
#End Region

#Region "�i�[��t�@�C���p�X"
    ''' <summary>
    ''' �i�[��t�@�C���p�X
    ''' </summary>
    ''' <remarks></remarks>
    Private strKakunousakiFilePass As String
    ''' <summary>
    ''' �i�[��t�@�C���p�X
    ''' </summary>
    ''' <value></value>
    ''' <returns>�i�[��t�@�C���p�X </returns>
    ''' <remarks></remarks>
    <TableMap("kakunousaki_file_pass")> _
    Public Property KakunousakiFilePass() As String
        Get
            Return strKakunousakiFilePass
        End Get
        Set(ByVal value As String)
            strKakunousakiFilePass = value
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
    <TableMap("add_login_user_id")> _
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
    <TableMap("add_datetime")> _
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
    <TableMap("upd_login_user_id")> _
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
    <TableMap("upd_datetime")> _
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