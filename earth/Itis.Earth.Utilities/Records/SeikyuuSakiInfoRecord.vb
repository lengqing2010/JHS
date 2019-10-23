Public Class SeikyuuSakiInfoRecord

#Region "請求先コード"
    ''' <summary>
    ''' 請求先コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiCd As String
    ''' <summary>
    ''' 請求先コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求先コード</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_cd")> _
    Public Property SeikyuuSakiCd() As String
        Get
            Return strSeikyuuSakiCd
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiCd = value
        End Set
    End Property
#End Region

#Region "請求先枝番"
    ''' <summary>
    ''' 請求先枝番
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiBrc As String
    ''' <summary>
    ''' 請求先枝番
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求先枝番</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_brc")> _
    Public Property SeikyuuSakiBrc() As String
        Get
            Return strSeikyuuSakiBrc
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiBrc = value
        End Set
    End Property
#End Region

#Region "請求先区分"
    ''' <summary>
    ''' 請求先区分
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiKbn As String
    ''' <summary>
    ''' 請求先区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求先区分</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_kbn")> _
    Public Property SeikyuuSakiKbn() As String
        Get
            Return strSeikyuuSakiKbn
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiKbn = value
        End Set
    End Property
#End Region

#Region "請求先名"
    ''' <summary>
    ''' 請求先名
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiMei As String
    ''' <summary>
    ''' 請求先名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求先名</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_mei")> _
    Public Property SeikyuuSakiMei() As String
        Get
            Return strSeikyuuSakiMei
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiMei = value
        End Set
    End Property
#End Region

#Region "請求先カナ"
    ''' <summary>
    ''' 請求先カナ
    ''' </summary>
    ''' <remarks></remarks>
    Private strSeikyuuSakiKana As String
    ''' <summary>
    ''' 請求先カナ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求先カナ</returns>
    ''' <remarks></remarks>
    <TableMap("seikyuu_saki_kana")> _
    Public Property SeikyuuSakiKana() As String
        Get
            Return strSeikyuuSakiKana
        End Get
        Set(ByVal value As String)
            strSeikyuuSakiKana = value
        End Set
    End Property
#End Region

#Region "請求書送付先住所1"
    ''' <summary>
    ''' 請求書送付先住所1
    ''' </summary>
    ''' <remarks></remarks>
    Private strSkysySoufuJyuusyo1 As String
    ''' <summary>
    ''' 請求書送付先住所1
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求書送付先住所1</returns>
    ''' <remarks></remarks>
    <TableMap("skysy_soufu_jyuusyo1")> _
    Public Property SkysySoufuJyuusyo1() As String
        Get
            Return strSkysySoufuJyuusyo1
        End Get
        Set(ByVal value As String)
            strSkysySoufuJyuusyo1 = value
        End Set
    End Property
#End Region

#Region "請求書送付先住所2"
    ''' <summary>
    ''' 請求書送付先住所2
    ''' </summary>
    ''' <remarks></remarks>
    Private strSkysySoufuJyuusyo2 As String
    ''' <summary>
    ''' 請求書送付先住所2
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求書送付先住所2</returns>
    ''' <remarks></remarks>
    <TableMap("skysy_soufu_jyuusyo2")> _
    Public Property SkysySoufuJyuusyo2() As String
        Get
            Return strSkysySoufuJyuusyo2
        End Get
        Set(ByVal value As String)
            strSkysySoufuJyuusyo2 = value
        End Set
    End Property
#End Region

#Region "請求書送付先郵便番号"
    ''' <summary>
    ''' 請求書送付先郵便番号
    ''' </summary>
    ''' <remarks></remarks>
    Private strSkysySoufuYuubinNo As String
    ''' <summary>
    ''' 請求書送付先郵便番号
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求書送付先郵便番号</returns>
    ''' <remarks></remarks>
    <TableMap("skysy_soufu_yuubin_no")> _
    Public Property SkysySoufuYuubinNo() As String
        Get
            Return strSkysySoufuYuubinNo
        End Get
        Set(ByVal value As String)
            strSkysySoufuYuubinNo = value
        End Set
    End Property
#End Region

#Region "請求書送付先電話番号"
    ''' <summary>
    ''' 請求書送付先電話番号
    ''' </summary>
    ''' <remarks></remarks>
    Private strSkysySoufuTelNo As String
    ''' <summary>
    ''' 請求書送付先電話番号
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求書送付先電話番号</returns>
    ''' <remarks></remarks>
    <TableMap("skysy_soufu_tel_no")> _
    Public Property SkysySoufuTelNo() As String
        Get
            Return strSkysySoufuTelNo
        End Get
        Set(ByVal value As String)
            strSkysySoufuTelNo = value
        End Set
    End Property
#End Region

#Region "請求書送付先FAX番号"
    ''' <summary>
    ''' 請求書送付先FAX番号
    ''' </summary>
    ''' <remarks></remarks>
    Private strSkysySoufuFaxNo As String
    ''' <summary>
    ''' 請求書送付先FAX番号
    ''' </summary>
    ''' <value></value>
    ''' <returns> 請求書送付先FAX番号</returns>
    ''' <remarks></remarks>
    <TableMap("skysy_soufu_fax_no")> _
    Public Property SkysySoufuFaxNo() As String
        Get
            Return strSkysySoufuFaxNo
        End Get
        Set(ByVal value As String)
            strSkysySoufuFaxNo = value
        End Set
    End Property
#End Region

End Class