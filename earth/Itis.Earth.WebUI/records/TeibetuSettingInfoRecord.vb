''' <summary>
''' 邸別データ修正の各明細コントロールに設定する<br/>
''' 共通データを保持するレコードクラスです
''' </summary>
''' <remarks></remarks>
Public Class TeibetuSettingInfoRecord

#Region "区分"
    ''' <summary>
    ''' 区分
    ''' </summary>
    ''' <remarks></remarks>
    Private _kubun As String
    ''' <summary>
    ''' 区分
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Kubun() As String
        Get
            Return _kubun
        End Get
        Set(ByVal value As String)
            _kubun = value
        End Set
    End Property
#End Region

#Region "番号（保証書NO）"
    ''' <summary>
    ''' 番号（保証書NO）
    ''' </summary>
    ''' <remarks></remarks>
    Private _bangou As String
    ''' <summary>
    ''' 番号（保証書NO）
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Bangou() As String
        Get
            Return _bangou
        End Get
        Set(ByVal value As String)
            _bangou = value
        End Set
    End Property
#End Region

#Region "ログインユーザーID"
    ''' <summary>
    ''' ログインユーザーID
    ''' </summary>
    ''' <remarks></remarks>
    Private _updLoginUserId As String
    ''' <summary>
    ''' ログインユーザーID
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UpdLoginUserId() As String
        Get
            Return _updLoginUserId
        End Get
        Set(ByVal value As String)
            _updLoginUserId = value
        End Set
    End Property
#End Region

#Region "発注書管理権限"
    ''' <summary>
    ''' 発注書管理権限
    ''' </summary>
    ''' <remarks></remarks>
    Private _hattyuusyoKanriKengen As Integer
    ''' <summary>
    ''' 発注書管理権限
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HattyuusyoKanriKengen() As Integer
        Get
            Return _hattyuusyoKanriKengen
        End Get
        Set(ByVal value As Integer)
            _hattyuusyoKanriKengen = value
        End Set
    End Property
#End Region

#Region "経理業務権限"
    ''' <summary>
    ''' 経理業務権限
    ''' </summary>
    ''' <remarks></remarks>
    Private _keiriGyoumuKengen As Integer
    ''' <summary>
    ''' 経理業務権限
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property KeiriGyoumuKengen() As Integer
        Get
            Return _keiriGyoumuKengen
        End Get
        Set(ByVal value As Integer)
            _keiriGyoumuKengen = value
        End Set
    End Property
#End Region

End Class
