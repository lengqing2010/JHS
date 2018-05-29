Public Class LoginUserInfo

#Region "ログインユーザーID"
    ''' <summary>
    ''' ログインユーザーID
    ''' </summary>
    ''' <remarks></remarks>
    Private strLoginUserId As String
    ''' <summary>
    ''' ログインユーザーID
    ''' </summary>
    ''' <value></value>
    ''' <returns>ログインユーザーID</returns>
    ''' <remarks></remarks>
    <TableMap("login_user_id")> _
    Public Property LoginUserId() As String
        Get
            Return strLoginUserId
        End Get
        Set(ByVal value As String)
            strLoginUserId = value
        End Set
    End Property
#End Region

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
    <TableMap("account_no")> _
    Public Property AccountNo() As Integer
        Get
            Return intAccountNo
        End Get
        Set(ByVal value As Integer)
            intAccountNo = value
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
    <TableMap("account")> _
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
    Private strName As String
    ''' <summary>
    ''' 氏名
    ''' </summary>
    ''' <value></value>
    ''' <returns>氏名</returns>
    ''' <remarks></remarks>
    <TableMap("simei")> _
    Public Property Name() As String
        Get
            Return strName
        End Get
        Set(ByVal value As String)
            strName = value
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
    <TableMap("bikou")> _
    Public Property Bikou() As String
        Get
            Return strBikou
        End Get
        Set(ByVal value As String)
            strBikou = value
        End Set
    End Property
#End Region

#Region "依頼業務権限"
    ''' <summary>
    ''' 依頼業務権限
    ''' </summary>
    ''' <remarks></remarks>
    Private intIraiGyoumuKengen As Integer
    ''' <summary>
    ''' 依頼業務権限
    ''' </summary>
    ''' <value></value>
    ''' <returns>依頼業務権限</returns>
    ''' <remarks></remarks>
    <TableMap("irai_gyoumu_kengen")> _
    Public Property IraiGyoumuKengen() As Integer
        Get
            Return intIraiGyoumuKengen
        End Get
        Set(ByVal value As Integer)
            intIraiGyoumuKengen = value
        End Set
    End Property
#End Region

#Region "結果業務権限"
    ''' <summary>
    ''' 結果業務権限
    ''' </summary>
    ''' <remarks></remarks>
    Private intKekkaGyoumuKengen As Integer
    ''' <summary>
    ''' 結果業務権限
    ''' </summary>
    ''' <value></value>
    ''' <returns>結果業務権限</returns>
    ''' <remarks></remarks>
    <TableMap("kekka_gyoumu_kengen")> _
    Public Property KekkaGyoumuKengen() As Integer
        Get
            Return intKekkaGyoumuKengen
        End Get
        Set(ByVal value As Integer)
            intKekkaGyoumuKengen = value
        End Set
    End Property
#End Region

#Region "保証業務権限"
    ''' <summary>
    ''' 保証業務権限
    ''' </summary>
    ''' <remarks></remarks>
    Private intHosyouGyoumuKengen As Integer
    ''' <summary>
    ''' 保証業務権限
    ''' </summary>
    ''' <value></value>
    ''' <returns>保証業務権限</returns>
    ''' <remarks></remarks>
    <TableMap("hosyou_gyoumu_kengen")> _
    Public Property HosyouGyoumuKengen() As Integer
        Get
            Return intHosyouGyoumuKengen
        End Get
        Set(ByVal value As Integer)
            intHosyouGyoumuKengen = value
        End Set
    End Property
#End Region

#Region "報告書業務権限"
    ''' <summary>
    ''' 報告書業務権限
    ''' </summary>
    ''' <remarks></remarks>
    Private intHoukokusyoGyoumuKengen As Integer
    ''' <summary>
    ''' 報告書業務権限
    ''' </summary>
    ''' <value></value>
    ''' <returns>報告書業務権限</returns>
    ''' <remarks></remarks>
    <TableMap("hkks_gyoumu_kengen")> _
    Public Property HoukokusyoGyoumuKengen() As Integer
        Get
            Return intHoukokusyoGyoumuKengen
        End Get
        Set(ByVal value As Integer)
            intHoukokusyoGyoumuKengen = value
        End Set
    End Property
#End Region

#Region "工事業務権限"
    ''' <summary>
    ''' 工事業務権限
    ''' </summary>
    ''' <remarks></remarks>
    Private intKoujiGyoumuKengen As Integer
    ''' <summary>
    ''' 工事業務権限
    ''' </summary>
    ''' <value></value>
    ''' <returns>工事業務権限</returns>
    ''' <remarks></remarks>
    <TableMap("koj_gyoumu_kengen")> _
    Public Property KoujiGyoumuKengen() As Integer
        Get
            Return intKoujiGyoumuKengen
        End Get
        Set(ByVal value As Integer)
            intKoujiGyoumuKengen = value
        End Set
    End Property
#End Region

#Region "経理業務権限"
    ''' <summary>
    ''' 経理業務権限
    ''' </summary>
    ''' <remarks></remarks>
    Private intKeiriGyoumuKengen As Integer
    ''' <summary>
    ''' 経理業務権限
    ''' </summary>
    ''' <value></value>
    ''' <returns>経理業務権限</returns>
    ''' <remarks></remarks>
    <TableMap("keiri_gyoumu_kengen")> _
    Public Property KeiriGyoumuKengen() As Integer
        Get
            Return intKeiriGyoumuKengen
        End Get
        Set(ByVal value As Integer)
            intKeiriGyoumuKengen = value
        End Set
    End Property
#End Region

#Region "解析マスタ管理権限"
    ''' <summary>
    ''' 解析マスタ管理権限
    ''' </summary>
    ''' <remarks></remarks>
    Private intKaisekiMasterKanriKengen As Integer
    ''' <summary>
    ''' 解析マスタ管理権限
    ''' </summary>
    ''' <value></value>
    ''' <returns>解析マスタ管理権限</returns>
    ''' <remarks></remarks>
    <TableMap("kaiseki_master_kanri_kengen")> _
    Public Property KaisekiMasterKanriKengen() As Integer
        Get
            Return intKaisekiMasterKanriKengen
        End Get
        Set(ByVal value As Integer)
            intKaisekiMasterKanriKengen = value
        End Set
    End Property
#End Region

#Region "営業マスタ管理権限"
    ''' <summary>
    ''' 営業マスタ管理権限
    ''' </summary>
    ''' <remarks></remarks>
    Private intEigyouMasterKanriKengen As Integer
    ''' <summary>
    ''' 営業マスタ管理権限
    ''' </summary>
    ''' <value></value>
    ''' <returns>営業マスタ管理権限</returns>
    ''' <remarks></remarks>
    <TableMap("eigyou_master_kanri_kengen")> _
    Public Property EigyouMasterKanriKengen() As Integer
        Get
            Return intEigyouMasterKanriKengen
        End Get
        Set(ByVal value As Integer)
            intEigyouMasterKanriKengen = value
        End Set
    End Property
#End Region

#Region "価格マスタ管理権限"
    ''' <summary>
    ''' 価格マスタ管理権限
    ''' </summary>
    ''' <remarks></remarks>
    Private intKakakuMasterKanriKengen As Integer
    ''' <summary>
    ''' 価格マスタ管理権限
    ''' </summary>
    ''' <value></value>
    ''' <returns>価格マスタ管理権限</returns>
    ''' <remarks></remarks>
    <TableMap("kkk_master_kanri_kengen")> _
    Public Property KakakuMasterKanriKengen() As Integer
        Get
            Return intKakakuMasterKanriKengen
        End Get
        Set(ByVal value As Integer)
            intKakakuMasterKanriKengen = value
        End Set
    End Property
#End Region

#Region "販促売上権限"
    ''' <summary>
    ''' 販促売上権限
    ''' </summary>
    ''' <remarks></remarks>
    Private intHansokuUriageKengen As Integer
    ''' <summary>
    ''' 販促売上権限
    ''' </summary>
    ''' <value></value>
    ''' <returns>販促売上権限</returns>
    ''' <remarks></remarks>
    <TableMap("hansoku_uri_kengen")> _
    Public Property HansokuUriageKengen() As Integer
        Get
            Return intHansokuUriageKengen
        End Get
        Set(ByVal value As Integer)
            intHansokuUriageKengen = value
        End Set
    End Property
#End Region

#Region "データ破棄権限"
    ''' <summary>
    ''' データ破棄権限
    ''' </summary>
    ''' <remarks></remarks>
    Private intDataHakiKengen As Integer
    ''' <summary>
    ''' データ破棄権限
    ''' </summary>
    ''' <value></value>
    ''' <returns>データ破棄権限</returns>
    ''' <remarks></remarks>
    <TableMap("data_haki_kengen")> _
    Public Property DataHakiKengen() As Integer
        Get
            Return intDataHakiKengen
        End Get
        Set(ByVal value As Integer)
            intDataHakiKengen = value
        End Set
    End Property
#End Region

#Region "システム管理者権限"
    ''' <summary>
    ''' システム管理者権限
    ''' </summary>
    ''' <remarks></remarks>
    Private intSystemKanrisyaKengen As Integer
    ''' <summary>
    ''' システム管理者権限
    ''' </summary>
    ''' <value></value>
    ''' <returns>システム管理者権限</returns>
    ''' <remarks></remarks>
    <TableMap("system_kanrisya_kengen")> _
    Public Property SystemKanrisyaKengen() As Integer
        Get
            Return intSystemKanrisyaKengen
        End Get
        Set(ByVal value As Integer)
            intSystemKanrisyaKengen = value
        End Set
    End Property
#End Region

#Region "新規入力権限"
    ''' <summary>
    ''' 新規入力権限
    ''' </summary>
    ''' <remarks></remarks>
    Private intSinkiNyuuryokuKengen As Integer
    ''' <summary>
    ''' 新規入力権限
    ''' </summary>
    ''' <value></value>
    ''' <returns>新規入力権限</returns>
    ''' <remarks></remarks>
    <TableMap("sinki_nyuuryoku_kengen")> _
    Public Property SinkiNyuuryokuKengen() As Integer
        Get
            Return intSinkiNyuuryokuKengen
        End Get
        Set(ByVal value As Integer)
            intSinkiNyuuryokuKengen = value
        End Set
    End Property
#End Region

#Region "発注書管理権限"
    ''' <summary>
    ''' 発注書管理権限
    ''' </summary>
    ''' <remarks></remarks>
    Private intHattyuusyoKanriKengen As Integer
    ''' <summary>
    ''' 発注書管理権限
    ''' </summary>
    ''' <value></value>
    ''' <returns>発注書管理権限</returns>
    ''' <remarks></remarks>
    <TableMap("hattyuusyo_kanri_kengen")> _
    Public Property HattyuusyoKanriKengen() As Integer
        Get
            Return intHattyuusyoKanriKengen
        End Get
        Set(ByVal value As Integer)
            intHattyuusyoKanriKengen = value
        End Set
    End Property
#End Region


#Region "部署名(Department)"
    ''' <summary>
    ''' 部署名(Department)
    ''' </summary>
    ''' <remarks></remarks>
    Private strDepartment As String
    ''' <summary>
    ''' 部署名(Department)
    ''' </summary>
    ''' <value></value>
    ''' <returns>部署名(Department)</returns>
    ''' <remarks></remarks>
    <TableMap("Department")> _
    Public Property Department() As String
        Get
            Return strDepartment
        End Get
        Set(ByVal value As String)
            strDepartment = value
        End Set
    End Property
#End Region

End Class
