''' <summary>
''' ReportIfΆέ`FbNpΜR[hNXΕ·
''' </summary>
''' <remarks></remarks>
Public Class ReportIfChkRecord

#Region "ΪqΤ"
    ''' <summary>
    ''' ΪqΤ
    ''' </summary>
    ''' <remarks></remarks>
    Private strKokyakuNo As String
    ''' <summary>
    ''' ΪqΤ
    ''' </summary>
    ''' <value></value>
    ''' <returns> ΪqΤ</returns>
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

#Region "ΪqΤ-ΗΤ(ρ)"
    ''' <summary>
    ''' ΪqΤ-ΗΤ(ρ)
    ''' </summary>
    ''' <remarks></remarks>
    Private intKokyakuBrc As Integer
    ''' <summary>
    ''' ΪqΤ-ΗΤ(ρ)
    ''' </summary>
    ''' <value></value>
    ''' <returns> ΪqΤ-ΗΤ(ρ)</returns>
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

#Region "Ϋ―ΨΤ"
    ''' <summary>
    ''' Ϋ―ΨΤ
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyoukenNo As String
    ''' <summary>
    ''' Ϋ―ΨΤ
    ''' </summary>
    ''' <value></value>
    ''' <returns> Ϋ―ΨΤ</returns>
    ''' <remarks></remarks>
    <TableMap("syouken_no")> _
    Public Property SyoukenNo() As String
        Get
            ' NULLΝσΙ·ι
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