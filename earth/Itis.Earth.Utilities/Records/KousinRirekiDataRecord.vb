''' <summary>
''' �X�V�����f�[�^���R�[�h�N���X/�X�V�����e�[�u�����
''' </summary>
''' <remarks>�X�V�����f�[�^�̊i�[���Ɏg�p���܂�</remarks>


Public Class KousinRirekiDataRecord


#Region "�X�V����"
    ''' <summary>
    ''' �X�V����
    ''' </summary>
    ''' <remarks></remarks>
    Private dateKousinNitiji As DateTime
    ''' <summary>
    ''' �X�V����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �X�V����</returns>
    ''' <remarks></remarks>
    <TableMap("upd_datetime")> _
    Public Property KousinNitiji() As DateTime
        Get
            Return dateKousinNitiji
        End Get
        Set(ByVal value As DateTime)
            dateKousinNitiji = value
        End Set
    End Property
#End Region

#Region "�敪"
    ''' <summary>
    ''' �敪
    ''' </summary>
    ''' <remarks></remarks>
    Private strKubun As String
    ''' <summary>
    ''' �敪
    ''' </summary>
    ''' <value></value>
    ''' <returns> �敪</returns>
    ''' <remarks></remarks>
    <TableMap("kbn")> _
    Public Property Kubun() As String
        Get
            Return strKubun
        End Get
        Set(ByVal value As String)
            strKubun = value
        End Set
    End Property
#End Region

#Region "�ۏ؏�No"
    ''' <summary>
    ''' �ۏ؏�No
    ''' </summary>
    ''' <remarks></remarks>
    Private strHosyousyoNo As String
    ''' <summary>
    ''' �ۏ؏�No
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ۏ؏�No</returns>
    ''' <remarks></remarks>
    <TableMap("hosyousyo_no")> _
    Public Property HosyousyoNo() As String
        Get
            Return strHosyousyoNo
        End Get
        Set(ByVal value As String)
            strHosyousyoNo = value
        End Set
    End Property
#End Region

#Region "�X�V����"
    ''' <summary>
    ''' �X�V����
    ''' </summary>
    ''' <remarks></remarks>
    Private strKousinKoumoku As String
    ''' <summary>
    ''' �X�V����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �X�V����</returns>
    ''' <remarks></remarks>
    <TableMap("upd_koumoku")> _
    Public Property KousinKoumoku() As String
        Get
            Return strKousinKoumoku
        End Get
        Set(ByVal value As String)
            strKousinKoumoku = value
        End Set
    End Property
#End Region

#Region "�X�V�O�l"
    ''' <summary>
    ''' �X�V�O�l
    ''' </summary>
    ''' <remarks></remarks>
    Private strKousinPreValue As String
    ''' <summary>
    ''' �X�V�O�l
    ''' </summary>
    ''' <value></value>
    ''' <returns> �X�V�O�l</returns>
    ''' <remarks></remarks>
    <TableMap("upd_mae_atai")> _
    Public Property KousinPreValue() As String
        Get
            Return strKousinPreValue
        End Get
        Set(ByVal value As String)
            strKousinPreValue = value
        End Set
    End Property
#End Region

#Region "�X�V��l"
    ''' <summary>
    ''' �X�V��l
    ''' </summary>
    ''' <remarks></remarks>
    Private strKousinPostValue As String
    ''' <summary>
    ''' �X�V��l
    ''' </summary>
    ''' <value></value>
    ''' <returns> �X�V��l</returns>
    ''' <remarks></remarks>
    <TableMap("upd_go_atai")> _
    Public Property KousinPostValue() As String
        Get
            Return strKousinPostValue
        End Get
        Set(ByVal value As String)
            strKousinPostValue = value
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
    <TableMap("kousinsya")> _
    Public Property Kousinsya() As String
        Get
            Return strKousinsya
        End Get
        Set(ByVal value As String)
            strKousinsya = value
        End Set
    End Property
#End Region

End Class
