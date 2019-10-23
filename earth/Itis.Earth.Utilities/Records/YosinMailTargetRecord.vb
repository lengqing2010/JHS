''' <summary>
''' 与信チェック結果メールの送信先取得用レコードクラス
''' </summary>
''' <remarks></remarks>
Public Class YosinMailTargetRecord

#Region "加盟店コード"
    ''' <summary>
    ''' 加盟店コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenCd As String
    ''' <summary>
    ''' 加盟店コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 加盟店コード</returns>
    ''' <remarks></remarks>
    <TableMap("kameiten_cd")> _
    Public Property KameitenCd() As String
        Get
            Return strKameitenCd
        End Get
        Set(ByVal value As String)
            strKameitenCd = value
        End Set
    End Property
#End Region

#Region "加盟店名1"
    ''' <summary>
    ''' 加盟店名1
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenMei1 As String
    ''' <summary>
    ''' 加盟店名1
    ''' </summary>
    ''' <value></value>
    ''' <returns> 加盟店名1</returns>
    ''' <remarks></remarks>
    <TableMap("kameiten_mei1")> _
    Public Property KameitenMei1() As String
        Get
            Return strKameitenMei1
        End Get
        Set(ByVal value As String)
            strKameitenMei1 = value
        End Set
    End Property
#End Region

#Region "名寄先コード"
    ''' <summary>
    ''' 名寄先コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strNayoseSakiCd As String
    ''' <summary>
    ''' 名寄先コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 名寄先コード</returns>
    ''' <remarks></remarks>
    <TableMap("nayose_saki_cd")> _
    Public Property NayoseSakiCd() As String
        Get
            Return strNayoseSakiCd
        End Get
        Set(ByVal value As String)
            strNayoseSakiCd = value
        End Set
    End Property
#End Region

#Region "名寄先名１"
    ''' <summary>
    ''' 名寄先名１
    ''' </summary>
    ''' <remarks></remarks>
    Private strNayoseSakiName1 As String
    ''' <summary>
    ''' 名寄先名１
    ''' </summary>
    ''' <value></value>
    ''' <returns> 名寄先名１</returns>
    ''' <remarks></remarks>
    <TableMap("nayose_saki_name1")> _
    Public Property NayoseSakiName1() As String
        Get
            Return strNayoseSakiName1
        End Get
        Set(ByVal value As String)
            strNayoseSakiName1 = value
        End Set
    End Property
#End Region

#Region "警告状況"
    ''' <summary>
    ''' 警告状況
    ''' </summary>
    ''' <remarks></remarks>
    Private intKeikokuJoukyou As Integer
    ''' <summary>
    ''' 警告状況
    ''' </summary>
    ''' <value></value>
    ''' <returns> 警告状況</returns>
    ''' <remarks></remarks>
    <TableMap("keikoku_joukyou")> _
    Public Property KeikokuJoukyou() As Integer
        Get
            Return intKeikokuJoukyou
        End Get
        Set(ByVal value As Integer)
            intKeikokuJoukyou = value
        End Set
    End Property
#End Region

#Region "名称"
    ''' <summary>
    ''' 名称
    ''' </summary>
    ''' <remarks></remarks>
    Private strMeisyou As String
    ''' <summary>
    ''' 名称
    ''' </summary>
    ''' <value></value>
    ''' <returns> 名称</returns>
    ''' <remarks></remarks>
    <TableMap("meisyou")> _
    Public Property Meisyou() As String
        Get
            Return strMeisyou
        End Get
        Set(ByVal value As String)
            strMeisyou = value
        End Set
    End Property
#End Region

#Region "NTアカウント"
    ''' <summary>
    ''' NTアカウント
    ''' </summary>
    ''' <remarks></remarks>
    Private strPrimaryWindowsNTAccount As String
    ''' <summary>
    ''' NTアカウント
    ''' </summary>
    ''' <value></value>
    ''' <returns> NTアカウント</returns>
    ''' <remarks></remarks>
    <TableMap("PrimaryWindowsNTAccount")> _
    Public Property PrimaryWindowsNTAccount() As String
        Get
            Return strPrimaryWindowsNTAccount
        End Get
        Set(ByVal value As String)
            strPrimaryWindowsNTAccount = value
        End Set
    End Property
#End Region

#Region "表示名"
    ''' <summary>
    ''' 表示名
    ''' </summary>
    ''' <remarks></remarks>
    Private strDisplayName As String
    ''' <summary>
    ''' 表示名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 表示名</returns>
    ''' <remarks></remarks>
    <TableMap("DisplayName")> _
    Public Property DisplayName() As String
        Get
            Return strDisplayName
        End Get
        Set(ByVal value As String)
            strDisplayName = value
        End Set
    End Property
#End Region

#Region "E-MAIL-ADDRESS"
    ''' <summary>
    ''' E-MAIL-ADDRESS
    ''' </summary>
    ''' <remarks></remarks>
    Private strEmailAddresses As String
    ''' <summary>
    ''' E-MAIL-ADDRESS
    ''' </summary>
    ''' <value></value>
    ''' <returns> E-MAIL-ADDRESS</returns>
    ''' <remarks></remarks>
    <TableMap("EmailAddresses")> _
    Public Property EmailAddresses() As String
        Get
            Return strEmailAddresses
        End Get
        Set(ByVal value As String)
            strEmailAddresses = value
        End Set
    End Property
#End Region

#Region "部署コード"
    ''' <summary>
    ''' 部署コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strBusyoCd As String
    ''' <summary>
    ''' 部署コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 部署コード</returns>
    ''' <remarks></remarks>
    <TableMap("busyo_cd")> _
    Public Property BusyoCd() As String
        Get
            Return strBusyoCd
        End Get
        Set(ByVal value As String)
            strBusyoCd = value
        End Set
    End Property
#End Region

#Region "部署名"
    ''' <summary>
    ''' 部署名
    ''' </summary>
    ''' <remarks></remarks>
    Private strBusyoMei As String
    ''' <summary>
    ''' 部署名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 部署名</returns>
    ''' <remarks></remarks>
    <TableMap("busyo_mei")> _
    Public Property BusyoMei() As String
        Get
            Return strBusyoMei
        End Get
        Set(ByVal value As String)
            strBusyoMei = value
        End Set
    End Property
#End Region

End Class