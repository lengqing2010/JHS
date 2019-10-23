''' <summary>
''' 与信チェック結果メールの送信先取得用レコードクラス
''' </summary>
''' <remarks></remarks>
Public Class YosinMailTargetDARecord

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
    Public Property NayoseSakiCd() As String
        Get
            Return strNayoseSakiCd
        End Get
        Set(ByVal value As String)
            strNayoseSakiCd = value
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
    Public Property KeikokuJoukyou() As Integer
        Get
            Return intKeikokuJoukyou
        End Get
        Set(ByVal value As Integer)
            intKeikokuJoukyou = value
        End Set
    End Property
#End Region

#Region "警告状況名称"
    ''' <summary>
    ''' 警告状況名称
    ''' </summary>
    ''' <remarks></remarks>
    Private strKeikokuJoukyouMeisyou As String
    ''' <summary>
    ''' 警告状況名称
    ''' </summary>
    ''' <value></value>
    ''' <returns> 警告状況名称</returns>
    ''' <remarks></remarks>
    Public Property KeikokuJoukyouMeisyou() As String
        Get
            Return strKeikokuJoukyouMeisyou
        End Get
        Set(ByVal value As String)
            strKeikokuJoukyouMeisyou = value
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
    Public Property NayoseSakiName1() As String
        Get
            Return strNayoseSakiName1
        End Get
        Set(ByVal value As String)
            strNayoseSakiName1 = value
        End Set
    End Property
#End Region

#Region "NTアカウント(ユーザーID)"
    ''' <summary>
    ''' NTアカウント(ユーザーID)
    ''' </summary>
    ''' <remarks></remarks>
    Private strPrimaryWindowsNTAccount As String
    ''' <summary>
    ''' NTアカウント(ユーザーID)
    ''' </summary>
    ''' <value></value>
    ''' <returns> NTアカウント(ユーザーID)</returns>
    ''' <remarks></remarks>
    Public Property PrimaryWindowsNTAccount() As String
        Get
            Return strPrimaryWindowsNTAccount
        End Get
        Set(ByVal value As String)
            strPrimaryWindowsNTAccount = value
        End Set
    End Property
#End Region

#Region "表示名(ユーザー名)"
    ''' <summary>
    ''' 表示名(ユーザー名)
    ''' </summary>
    ''' <remarks></remarks>
    Private strDisplayName As String
    ''' <summary>
    ''' 表示名(ユーザー名)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 表示名(ユーザー名)</returns>
    ''' <remarks></remarks>
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