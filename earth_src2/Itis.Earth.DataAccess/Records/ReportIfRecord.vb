''' <summary>
''' ReportIf登録・更新データ設定用のレコードクラスです
''' </summary>
''' <remarks></remarks>
Public Class ReportIfRecord

#Region "顧客番号"
    ''' <summary>
    ''' 顧客番号
    ''' </summary>
    ''' <remarks></remarks>
    Private strKokyakuNo As String
    ''' <summary>
    ''' 顧客番号
    ''' </summary>
    ''' <value></value>
    ''' <returns> 顧客番号</returns>
    ''' <remarks></remarks>
    Public Property KokyakuNo() As String
        Get
            Return strKokyakuNo
        End Get
        Set(ByVal value As String)
            strKokyakuNo = value
        End Set
    End Property
#End Region

#Region "顧客番号-追番(回数)"
    ''' <summary>
    ''' 顧客番号-追番(回数)
    ''' </summary>
    ''' <remarks></remarks>
    Private intKokyakuBrc As Integer
    ''' <summary>
    ''' 顧客番号-追番(回数)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 顧客番号-追番(回数)</returns>
    ''' <remarks></remarks>
    Public Property KokyakuBrc() As Integer
        Get
            Return intKokyakuBrc
        End Get
        Set(ByVal value As Integer)
            intKokyakuBrc = value
        End Set
    End Property
#End Region

#Region "サービス区分"
    ''' <summary>
    ''' サービス区分
    ''' </summary>
    ''' <remarks></remarks>
    Private intServiceKbn As Integer
    ''' <summary>
    ''' サービス区分
    ''' </summary>
    ''' <value></value>
    ''' <returns> サービス区分</returns>
    ''' <remarks></remarks>
    Public Property ServiceKbn() As Integer
        Get
            Return intServiceKbn
        End Get
        Set(ByVal value As Integer)
            intServiceKbn = value
        End Set
    End Property
#End Region

#Region "保険証券番号"
    ''' <summary>
    ''' 保険証券番号
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyoukenNo As String
    ''' <summary>
    ''' 保険証券番号
    ''' </summary>
    ''' <value></value>
    ''' <returns> 保険証券番号</returns>
    ''' <remarks></remarks>
    Public Property SyoukenNo() As String
        Get
            Return strSyoukenNo
        End Get
        Set(ByVal value As String)
            strSyoukenNo = value
        End Set
    End Property
#End Region

#Region "調査方法"
    ''' <summary>
    ''' 調査方法
    ''' </summary>
    ''' <remarks></remarks>
    Private strTyousa As String
    ''' <summary>
    ''' 調査方法
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査方法</returns>
    ''' <remarks></remarks>
    Public Property Tyousa() As String
        Get
            Return strTyousa
        End Get
        Set(ByVal value As String)
            strTyousa = value
        End Set
    End Property
#End Region

#Region "計画建物"
    ''' <summary>
    ''' 計画建物
    ''' </summary>
    ''' <remarks></remarks>
    Private strKeikaku As String
    ''' <summary>
    ''' 計画建物
    ''' </summary>
    ''' <value></value>
    ''' <returns> 計画建物</returns>
    ''' <remarks></remarks>
    Public Property Keikaku() As String
        Get
            Return strKeikaku
        End Get
        Set(ByVal value As String)
            strKeikaku = value
        End Set
    End Property
#End Region

#Region "施主名"
    ''' <summary>
    ''' 施主名
    ''' </summary>
    ''' <remarks></remarks>
    Private strSesyuName As String
    ''' <summary>
    ''' 施主名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 施主名</returns>
    ''' <remarks></remarks>
    Public Property SesyuName() As String
        Get
            Return strSesyuName
        End Get
        Set(ByVal value As String)
            strSesyuName = value
        End Set
    End Property
#End Region

#Region "物件住所1(1行目)"
    ''' <summary>
    ''' 物件住所1(1行目)
    ''' </summary>
    ''' <remarks></remarks>
    Private strBknAdr1 As String
    ''' <summary>
    ''' 物件住所1(1行目)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 物件住所1(1行目)</returns>
    ''' <remarks></remarks>
    Public Property BknAdr1() As String
        Get
            Return strBknAdr1
        End Get
        Set(ByVal value As String)
            strBknAdr1 = value
        End Set
    End Property
#End Region

#Region "物件住所2(2行目)"
    ''' <summary>
    ''' 物件住所2(2行目)
    ''' </summary>
    ''' <remarks></remarks>
    Private strBknAdr2 As String
    ''' <summary>
    ''' 物件住所2(2行目)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 物件住所2(2行目)</returns>
    ''' <remarks></remarks>
    Public Property BknAdr2() As String
        Get
            Return strBknAdr2
        End Get
        Set(ByVal value As String)
            strBknAdr2 = value
        End Set
    End Property
#End Region

#Region "調査希望日"
    ''' <summary>
    ''' 調査希望日
    ''' </summary>
    ''' <remarks></remarks>
    Private dateChousaHopeDate As DateTime
    ''' <summary>
    ''' 調査希望日
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査希望日</returns>
    ''' <remarks></remarks>
    Public Property ChousaHopeDate() As DateTime
        Get
            Return dateChousaHopeDate
        End Get
        Set(ByVal value As DateTime)
            dateChousaHopeDate = value
        End Set
    End Property
#End Region

#Region "調査希望日(時間等)"
    ''' <summary>
    ''' 調査希望日(時間等)
    ''' </summary>
    ''' <remarks></remarks>
    Private strChousaHopeTime As String
    ''' <summary>
    ''' 調査希望日(時間等)
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査希望日(時間等)</returns>
    ''' <remarks></remarks>
    Public Property ChousaHopeTime() As String
        Get
            Return strChousaHopeTime
        End Get
        Set(ByVal value As String)
            strChousaHopeTime = value
        End Set
    End Property
#End Region

#Region "調査依頼担当者"
    ''' <summary>
    ''' 調査依頼担当者
    ''' </summary>
    ''' <remarks></remarks>
    Private strChousaTanto As String
    ''' <summary>
    ''' 調査依頼担当者
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査依頼担当者</returns>
    ''' <remarks></remarks>
    Public Property ChousaTanto() As String
        Get
            Return strChousaTanto
        End Get
        Set(ByVal value As String)
            strChousaTanto = value
        End Set
    End Property
#End Region

#Region "調査立会者"
    ''' <summary>
    ''' 調査立会者
    ''' </summary>
    ''' <remarks></remarks>
    Private strChousaTachiai As String
    ''' <summary>
    ''' 調査立会者
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査立会者</returns>
    ''' <remarks></remarks>
    Public Property ChousaTachiai() As String
        Get
            Return strChousaTachiai
        End Get
        Set(ByVal value As String)
            strChousaTachiai = value
        End Set
    End Property
#End Region

#Region "加盟店コード"
    ''' <summary>
    ''' 加盟店コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameiCd As String
    ''' <summary>
    ''' 加盟店コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 加盟店コード</returns>
    ''' <remarks></remarks>
    Public Property KameiCd() As String
        Get
            Return strKameiCd
        End Get
        Set(ByVal value As String)
            strKameiCd = value
        End Set
    End Property
#End Region

#Region "加盟店名"
    ''' <summary>
    ''' 加盟店名
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameiName As String
    ''' <summary>
    ''' 加盟店名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 加盟店名</returns>
    ''' <remarks></remarks>
    Public Property KameiName() As String
        Get
            Return strKameiName
        End Get
        Set(ByVal value As String)
            strKameiName = value
        End Set
    End Property
#End Region

#Region "加盟店TEL"
    ''' <summary>
    ''' 加盟店TEL
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameiTel As String
    ''' <summary>
    ''' 加盟店TEL
    ''' </summary>
    ''' <value></value>
    ''' <returns> 加盟店TEL</returns>
    ''' <remarks></remarks>
    Public Property KameiTel() As String
        Get
            Return strKameiTel
        End Get
        Set(ByVal value As String)
            strKameiTel = value
        End Set
    End Property
#End Region

#Region "加盟店FAX"
    ''' <summary>
    ''' 加盟店FAX
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameiFax As String
    ''' <summary>
    ''' 加盟店FAX
    ''' </summary>
    ''' <value></value>
    ''' <returns> 加盟店FAX</returns>
    ''' <remarks></remarks>
    Public Property KameiFax() As String
        Get
            Return strKameiFax
        End Get
        Set(ByVal value As String)
            strKameiFax = value
        End Set
    End Property
#End Region

#Region "加盟店MAIL"
    ''' <summary>
    ''' 加盟店MAIL
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameiMail As String
    ''' <summary>
    ''' 加盟店MAIL
    ''' </summary>
    ''' <value></value>
    ''' <returns> 加盟店MAIL</returns>
    ''' <remarks></remarks>
    Public Property KameiMail() As String
        Get
            Return strKameiMail
        End Get
        Set(ByVal value As String)
            strKameiMail = value
        End Set
    End Property
#End Region

#Region "調査会社コード"
    ''' <summary>
    ''' 調査会社コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strTyousaCd As String
    ''' <summary>
    ''' 調査会社コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査会社コード</returns>
    ''' <remarks></remarks>
    Public Property TyousaCd() As String
        Get
            Return strTyousaCd
        End Get
        Set(ByVal value As String)
            strTyousaCd = value
        End Set
    End Property
#End Region

#Region "調査会社事業所コード"
    ''' <summary>
    ''' 調査会社事業所コード
    ''' </summary>
    ''' <remarks></remarks>
    Private strTyousaBrc As String
    ''' <summary>
    ''' 調査会社事業所コード
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査会社事業所コード</returns>
    ''' <remarks></remarks>
    Public Property TyousaBrc() As String
        Get
            Return strTyousaBrc
        End Get
        Set(ByVal value As String)
            strTyousaBrc = value
        End Set
    End Property
#End Region

#Region "調査会社名称"
    ''' <summary>
    ''' 調査会社名称
    ''' </summary>
    ''' <remarks></remarks>
    Private strTyousaName As String
    ''' <summary>
    ''' 調査会社名称
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査会社名称</returns>
    ''' <remarks></remarks>
    Public Property TyousaName() As String
        Get
            Return strTyousaName
        End Get
        Set(ByVal value As String)
            strTyousaName = value
        End Set
    End Property
#End Region

#Region "レポートSS入力制御のレコード更新タイムスタンプ"
    ''' <summary>
    ''' レポートSS入力制御のレコード更新タイムスタンプ
    ''' </summary>
    ''' <remarks></remarks>
    Private dateReportUpdateTime As DateTime
    ''' <summary>
    ''' レポートSS入力制御のレコード更新タイムスタンプ
    ''' </summary>
    ''' <value></value>
    ''' <returns> レポートSS入力制御のレコード更新タイムスタンプ</returns>
    ''' <remarks></remarks>
    Public Property ReportUpdateTime() As DateTime
        Get
            ' 未設定時はシステム日付を返却
            If dateReportUpdateTime = DateTime.MinValue Then
                dateReportUpdateTime = DateTime.Now
            End If

            Return dateReportUpdateTime

        End Get
        Set(ByVal value As DateTime)
            dateReportUpdateTime = value
        End Set
    End Property
#End Region

#Region "進捗データ送信ステータス"
    ''' <summary>
    ''' 進捗データ送信ステータス
    ''' </summary>
    ''' <remarks></remarks>
    Private strSendSts As String = "00"
    ''' <summary>
    ''' 進捗データ送信ステータス
    ''' </summary>
    ''' <value></value>
    ''' <returns> 進捗データ送信ステータス</returns>
    ''' <remarks></remarks>
    Public Property SendSts() As String
        Get
            Return strSendSts
        End Get
        Set(ByVal value As String)
            strSendSts = value
        End Set
    End Property
#End Region

#Region "進捗データ受信ステータス"
    ''' <summary>
    ''' 進捗データ受信ステータス
    ''' </summary>
    ''' <remarks></remarks>
    Private strRecvSts As String = "00"
    ''' <summary>
    ''' 進捗データ受信ステータス
    ''' </summary>
    ''' <value></value>
    ''' <returns> 進捗データ受信ステータス</returns>
    ''' <remarks></remarks>
    Public Property RecvSts() As String
        Get
            Return strRecvSts
        End Get
        Set(ByVal value As String)
            strRecvSts = value
        End Set
    End Property
#End Region

#Region "PDF受信ステータス"
    ''' <summary>
    ''' PDF受信ステータス
    ''' </summary>
    ''' <remarks></remarks>
    Private strPdfSts As String = "00"
    ''' <summary>
    ''' PDF受信ステータス
    ''' </summary>
    ''' <value></value>
    ''' <returns> PDF受信ステータス</returns>
    ''' <remarks></remarks>
    Public Property PdfSts() As String
        Get
            Return strPdfSts
        End Get
        Set(ByVal value As String)
            strPdfSts = value
        End Set
    End Property
#End Region

#Region "調査連絡先_宛先"
    ''' <summary>
    ''' 調査連絡先_宛先
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysRenrakusakiAtesakiMei As String
    ''' <summary>
    ''' 調査連絡先_宛先
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査連絡先_宛先</returns>
    ''' <remarks></remarks>
    Public Property TysRenrakusakiAtesakiMei() As String
        Get
            Return strTysRenrakusakiAtesakiMei
        End Get
        Set(ByVal value As String)
            strTysRenrakusakiAtesakiMei = value
        End Set
    End Property
#End Region

#Region "調査連絡先_TEL"
    ''' <summary>
    ''' 調査連絡先_TEL
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysRenrakusakiTel As String
    ''' <summary>
    ''' 調査連絡先_TEL
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査連絡先_TEL</returns>
    ''' <remarks></remarks>
    Public Property TysRenrakusakiTel() As String
        Get
            Return strTysRenrakusakiTel
        End Get
        Set(ByVal value As String)
            strTysRenrakusakiTel = value
        End Set
    End Property
#End Region

#Region "調査連絡先_FAX"
    ''' <summary>
    ''' 調査連絡先_FAX
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysRenrakusakiFax As String
    ''' <summary>
    ''' 調査連絡先_FAX
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査連絡先_FAX</returns>
    ''' <remarks></remarks>
    Public Property TysRenrakusakiFax() As String
        Get
            Return strTysRenrakusakiFax
        End Get
        Set(ByVal value As String)
            strTysRenrakusakiFax = value
        End Set
    End Property
#End Region

#Region "調査連絡先_MAIL"
    ''' <summary>
    ''' 調査連絡先_MAIL
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysRenrakusakiMail As String
    ''' <summary>
    ''' 調査連絡先_MAIL
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査連絡先_MAIL</returns>
    ''' <remarks></remarks>
    Public Property TysRenrakusakiMail() As String
        Get
            Return strTysRenrakusakiMail
        End Get
        Set(ByVal value As String)
            strTysRenrakusakiMail = value
        End Set
    End Property
#End Region

#Region "調査連絡先_担当者"
    ''' <summary>
    ''' 調査連絡先_担当者
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysRenrakusakiTantouMei As String
    ''' <summary>
    ''' 調査連絡先_担当者
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査連絡先_担当者</returns>
    ''' <remarks></remarks>
    Public Property TysRenrakusakiTantouMei() As String
        Get
            Return strTysRenrakusakiTantouMei
        End Get
        Set(ByVal value As String)
            strTysRenrakusakiTantouMei = value
        End Set
    End Property
#End Region

End Class