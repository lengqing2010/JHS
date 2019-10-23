''' <summary>
''' ReportIf‘¶İƒ`ƒFƒbƒN—p‚ÌƒŒƒR[ƒhƒNƒ‰ƒX‚Å‚·
''' </summary>
''' <remarks></remarks>
Public Class ReportIfChkRecord

#Region "ŒÚ‹q”Ô†"
    ''' <summary>
    ''' ŒÚ‹q”Ô†
    ''' </summary>
    ''' <remarks></remarks>
    Private strKokyakuNo As String
    ''' <summary>
    ''' ŒÚ‹q”Ô†
    ''' </summary>
    ''' <value></value>
    ''' <returns> ŒÚ‹q”Ô†</returns>
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

#Region "ŒÚ‹q”Ô†-’Ç”Ô(‰ñ”)"
    ''' <summary>
    ''' ŒÚ‹q”Ô†-’Ç”Ô(‰ñ”)
    ''' </summary>
    ''' <remarks></remarks>
    Private intKokyakuBrc As Integer
    ''' <summary>
    ''' ŒÚ‹q”Ô†-’Ç”Ô(‰ñ”)
    ''' </summary>
    ''' <value></value>
    ''' <returns> ŒÚ‹q”Ô†-’Ç”Ô(‰ñ”)</returns>
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

#Region "•ÛŒ¯ØŒ””Ô†"
    ''' <summary>
    ''' •ÛŒ¯ØŒ””Ô†
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyoukenNo As String
    ''' <summary>
    ''' •ÛŒ¯ØŒ””Ô†
    ''' </summary>
    ''' <value></value>
    ''' <returns> •ÛŒ¯ØŒ””Ô†</returns>
    ''' <remarks></remarks>
    <TableMap("syouken_no")> _
    Public Property SyoukenNo() As String
        Get
            ' NULL‚Í‹ó”’‚É‚·‚é
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