''' <summary>
''' ReportIf���݃`�F�b�N�p�̃��R�[�h�N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class ReportIfChkRecord

#Region "�ڋq�ԍ�"
    ''' <summary>
    ''' �ڋq�ԍ�
    ''' </summary>
    ''' <remarks></remarks>
    Private strKokyakuNo As String
    ''' <summary>
    ''' �ڋq�ԍ�
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ڋq�ԍ�</returns>
    ''' <remarks></remarks>
    <TableMap("kokyaku_no")> _
    Public Property KokyakuNo() As String
        Get
            Return strKokyakuNo
        End Get
        Set(ByVal value As String)
            strKokyakuNo = value
        End Set
    End Property
#End Region

#Region "�ڋq�ԍ�-�ǔ�(��)"
    ''' <summary>
    ''' �ڋq�ԍ�-�ǔ�(��)
    ''' </summary>
    ''' <remarks></remarks>
    Private intKokyakuBrc As Integer
    ''' <summary>
    ''' �ڋq�ԍ�-�ǔ�(��)
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ڋq�ԍ�-�ǔ�(��)</returns>
    ''' <remarks></remarks>
    <TableMap("kokyaku_brc")> _
    Public Property KokyakuBrc() As Integer
        Get
            Return intKokyakuBrc
        End Get
        Set(ByVal value As Integer)
            intKokyakuBrc = value
        End Set
    End Property
#End Region

#Region "�ی��،��ԍ�"
    ''' <summary>
    ''' �ی��،��ԍ�
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyoukenNo As String
    ''' <summary>
    ''' �ی��،��ԍ�
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ی��،��ԍ�</returns>
    ''' <remarks></remarks>
    <TableMap("syouken_no")> _
    Public Property SyoukenNo() As String
        Get
            ' NULL�͋󔒂ɂ���
            If strSyoukenNo Is Nothing Then
                strSyoukenNo = ""
            End If

            Return strSyoukenNo
        End Get
        Set(ByVal value As String)
            strSyoukenNo = value
        End Set
    End Property
#End Region

End Class