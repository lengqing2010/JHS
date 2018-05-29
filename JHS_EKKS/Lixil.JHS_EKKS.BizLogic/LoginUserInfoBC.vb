''' <summary>
'''   ユーザーINFORリスト
''' </summary>
''' <remarks></remarks>
Public Class LoginUserInfoList
    Private _LoginUserInfoList As New List(Of LoginUserInfoBC)
    Public Property Items() As List(Of LoginUserInfoBC)
        Get
            Return _LoginUserInfoList
        End Get
        Set(ByVal value As List(Of LoginUserInfoBC))
            _LoginUserInfoList = value
        End Set
    End Property
End Class
''' <summary>
''' ユーザーINFOR
''' </summary>
''' <remarks></remarks>
Public Class LoginUserInfoBC
#Region "アカウントNO"
    ''' <summary>
    ''' アカウントNO
    ''' </summary>
    ''' <remarks></remarks>
    Private intAccountNo As Integer
    ''' <summary>
    ''' アカウントNO
    ''' </summary>
    ''' <value></value>
    ''' <returns>アカウントNO</returns>
    ''' <remarks></remarks>
    Public Property AccountNo() As Integer
        Get
            Return intAccountNo
        End Get
        Set(ByVal value As Integer)
            intAccountNo = value
        End Set
    End Property
#End Region

#Region "取消"
    ''' <summary>
    ''' 取消
    ''' </summary>
    ''' <remarks></remarks>
    Private intTorikesi As Integer
    ''' <summary>
    ''' 取消
    ''' </summary>
    ''' <value></value>
    ''' <returns>取消</returns>
    ''' <remarks></remarks>
    Public Property Torikesi() As Integer
        Get
            Return intTorikesi
        End Get
        Set(ByVal value As Integer)
            intTorikesi = value
        End Set
    End Property
#End Region
#Region "アカウント"
    ''' <summary>
    ''' アカウント
    ''' </summary>
    ''' <remarks></remarks>
    Private strAccount As String
    ''' <summary>
    ''' アカウント
    ''' </summary>
    ''' <value></value>
    ''' <returns>アカウント</returns>
    ''' <remarks></remarks>
    Public Property Account() As String
        Get
            Return strAccount
        End Get
        Set(ByVal value As String)
            strAccount = value
        End Set
    End Property
#End Region
#Region "氏名"
    ''' <summary>
    ''' 氏名
    ''' </summary>
    ''' <remarks></remarks>
    Private strSimei As String
    ''' <summary>
    ''' 氏名
    ''' </summary>
    ''' <value></value>
    ''' <returns>氏名</returns>
    ''' <remarks></remarks>
    Public Property Simei() As String
        Get
            Return strSimei
        End Get
        Set(ByVal value As String)
            strSimei = value
        End Set
    End Property
#End Region

#Region "所属部署"
    ''' <summary>
    ''' 所属部署
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyozokuBusyo As String
    ''' <summary>
    ''' 所属部署
    ''' </summary>
    ''' <value></value>
    ''' <returns>所属部署</returns>
    ''' <remarks></remarks>

    Public Property SyozokuBusyo() As String
        Get
            Return strSyozokuBusyo
        End Get
        Set(ByVal value As String)
            strSyozokuBusyo = value
        End Set
    End Property
#End Region
#Region "備考"
    ''' <summary>
    ''' 備考
    ''' </summary>
    ''' <remarks></remarks>
    Private strBikou As String
    ''' <summary>
    ''' 備考
    ''' </summary>
    ''' <value></value>
    ''' <returns>備考</returns>
    ''' <remarks></remarks>
    Public Property Bikou() As String
        Get
            Return strBikou
        End Get
        Set(ByVal value As String)
            strBikou = value
        End Set
    End Property
#End Region

#Region "営業計画管理_参照権限"
    ''' <summary>
    ''' 営業計画管理_参照権限
    ''' </summary>
    ''' <remarks></remarks>
    Private intEigyouKeikakuKenriSansyou As Integer
    ''' <summary>
    ''' 営業計画管理_参照権限
    ''' </summary>
    ''' <value></value>
    ''' <returns>営業計画管理_参照権限</returns>
    ''' <remarks></remarks>
    Public Property EigyouKeikakuKenriSansyou() As Integer
        Get
            Return intEigyouKeikakuKenriSansyou
        End Get
        Set(ByVal value As Integer)
            intEigyouKeikakuKenriSansyou = value
        End Set
    End Property
#End Region
#Region "売上予実_参照権限"
    ''' <summary>
    ''' 売上予実_参照権限
    ''' </summary>
    ''' <remarks></remarks>
    Private intUriYojituKanriSansyou As Integer
    ''' <summary>
    ''' 売上予実_参照権限
    ''' </summary>
    ''' <value></value>
    ''' <returns>売上予実_参照権限</returns>
    ''' <remarks></remarks>
    Public Property UriYojituKanriSansyou() As Integer
        Get
            Return intUriYojituKanriSansyou
        End Get
        Set(ByVal value As Integer)
            intUriYojituKanriSansyou = value
        End Set
    End Property
#End Region
#Region "全社計画_確定権限"
    ''' <summary>
    ''' 全社計画_確定権限
    ''' </summary>
    ''' <remarks></remarks>
    Private intZensyaKeikakuKengen As Integer
    ''' <summary>
    ''' 全社計画_確定権限
    ''' </summary>
    ''' <value></value>
    ''' <returns>全社計画_確定権限</returns>
    ''' <remarks></remarks>
    Public Property ZensyaKeikakuKengen() As Integer
        Get
            Return intZensyaKeikakuKengen
        End Get
        Set(ByVal value As Integer)
            intZensyaKeikakuKengen = value
        End Set
    End Property
#End Region

#Region "支店別年度計画_確定権限"
    ''' <summary>
    '''支店別年度計画_確定権限
    ''' </summary>
    ''' <remarks></remarks>
    Private intSitenbetuNenKeikakuKengen As Integer
    ''' <summary>
    ''' 支店別年度計画_確定権限
    ''' </summary>
    ''' <value></value>
    ''' <returns>支店別年度計画_確定権限</returns>
    ''' <remarks></remarks>
    Public Property SitenbetuNenKeikakuKengen() As Integer
        Get
            Return intSitenbetuNenKeikakuKengen
        End Get
        Set(ByVal value As Integer)
            intSitenbetuNenKeikakuKengen = value
        End Set
    End Property
#End Region

#Region "支店別月次計画_取込権限"
    ''' <summary>
    '''支店別月次計画_取込権限
    ''' </summary>
    ''' <remarks></remarks>
    Private intSitenbetuGetujiKeikakuTorikomi As Integer
    ''' <summary>
    ''' 支店別月次計画_取込権限
    ''' </summary>
    ''' <value></value>
    ''' <returns>支店別月次計画_取込権限</returns>
    ''' <remarks></remarks>
    Public Property SitenbetuGetujiKeikakuTorikomi() As Integer
        Get
            Return intSitenbetuGetujiKeikakuTorikomi
        End Get
        Set(ByVal value As Integer)
            intSitenbetuGetujiKeikakuTorikomi = value
        End Set
    End Property
#End Region

#Region "支店別月次計画_確定権限"
    ''' <summary>
    '''支店別月次計画_確定権限
    ''' </summary>
    ''' <remarks></remarks>
    Private intSitenbetuGetujiKeikakuKakutei As Integer
    ''' <summary>
    ''' 支店別月次計画_確定権限
    ''' </summary>
    ''' <value></value>
    ''' <returns>支店別月次計画_確定権限</returns>
    ''' <remarks></remarks>
    Public Property SitenbetuGetujiKeikakuKakutei() As Integer
        Get
            Return intSitenbetuGetujiKeikakuKakutei
        End Get
        Set(ByVal value As Integer)
            intSitenbetuGetujiKeikakuKakutei = value
        End Set
    End Property
#End Region

#Region "支店別月次計画_見直し権限"
    ''' <summary>
    '''支店別月次計画_見直し権限
    ''' </summary>
    ''' <remarks></remarks>
    Private intSitenbetuGetujiKeikakuMinaosi As Integer
    ''' <summary>
    ''' 支店別月次計画_見直し権限
    ''' </summary>
    ''' <value></value>
    ''' <returns>支店別月次計画_見直し権限</returns>
    ''' <remarks></remarks>
    Public Property SitenbetuGetujiKeikakuMinaosi() As Integer
        Get
            Return intSitenbetuGetujiKeikakuMinaosi
        End Get
        Set(ByVal value As Integer)
            intSitenbetuGetujiKeikakuMinaosi = value
        End Set
    End Property
#End Region

#Region "計画値見直し_権限"
    ''' <summary>
    ''' 計画値見直し_権限
    ''' </summary>
    ''' <remarks></remarks>
    Private intKeikakuMinaosiKengen As Integer
    ''' <summary>
    ''' 計画値見直し_権限
    ''' </summary>
    ''' <value></value>
    ''' <returns>計画値見直し_権限</returns>
    ''' <remarks></remarks>
    Public Property KeikakuMinaosiKengen() As Integer
        Get
            Return intKeikakuMinaosiKengen
        End Get
        Set(ByVal value As Integer)
            intKeikakuMinaosiKengen = value
        End Set
    End Property
#End Region

#Region "計画値確定_権限"
    ''' <summary>
    ''' 計画値確定_権限
    ''' </summary>
    ''' <remarks></remarks>
    Private intKeikakuKakuteiKengen As Integer
    ''' <summary>
    '''計画値確定_権限
    ''' </summary>
    ''' <value></value>
    ''' <returns>計画値確定_権限</returns>
    ''' <remarks></remarks>
    Public Property KeikakuKakuteiKengen() As Integer
        Get
            Return intKeikakuKakuteiKengen
        End Get
        Set(ByVal value As Integer)
            intKeikakuKakuteiKengen = value
        End Set
    End Property
#End Region

#Region "支店別月別計画_確定権限"
    ''' <summary>
    ''' 支店別月別計画_確定権限
    ''' </summary>
    ''' <remarks></remarks>
    Private intKeikakuTorikomiKengen As Integer
    ''' <summary>
    '''支店別月別計画_確定権限
    ''' </summary>
    ''' <value></value>
    ''' <returns>支店別月別計画_確定権限</returns>
    ''' <remarks></remarks>
    Public Property KeikakuTorikomiKengen() As Integer
        Get
            Return intKeikakuTorikomiKengen
        End Get
        Set(ByVal value As Integer)
            intKeikakuTorikomiKengen = value
        End Set
    End Property
#End Region

End Class
