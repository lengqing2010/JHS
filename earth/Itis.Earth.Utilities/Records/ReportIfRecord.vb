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

#Region "調査方法NO"
    ''' <summary>
    ''' 調査方法NO
    ''' </summary>
    ''' <remarks></remarks>
    Private intTyousaNo As Integer
    ''' <summary>
    ''' 調査方法NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 調査方法NO</returns>
    ''' <remarks></remarks>
    Public Property TyousaNo() As Integer
        Get
            Return intTyousaNo
        End Get
        Set(ByVal value As Integer)
            intTyousaNo = value
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

#Region "構造NO"
    ''' <summary>
    ''' 構造NO
    ''' </summary>
    ''' <remarks></remarks>
    Private intKouzouNo As Integer
    ''' <summary>
    ''' 構造NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> 構造NO</returns>
    ''' <remarks></remarks>
    Public Property KouzouNo() As Integer
        Get
            Return intKouzouNo
        End Get
        Set(ByVal value As Integer)
            intKouzouNo = value
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

#Region "加盟店カナ名称１"
    ''' <summary>
    ''' 加盟店カナ名称１
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenmeiKana1 As String
    ''' <summary>
    ''' 加盟店カナ名称１
    ''' </summary>
    ''' <value></value>
    ''' <returns> 加盟店カナ名称１</returns>
    ''' <remarks></remarks>
    Public Property KameitenmeiKana1() As String
        Get
            Return strKameitenmeiKana1
        End Get
        Set(ByVal value As String)
            strKameitenmeiKana1 = value
        End Set
    End Property
#End Region

#Region "構造"
    ''' <summary>
    ''' 構造
    ''' </summary>
    ''' <remarks></remarks>
    Private strKouzou As String
    ''' <summary>
    ''' 構造
    ''' </summary>
    ''' <value></value>
    ''' <returns> 構造</returns>
    ''' <remarks></remarks>
    Public Property Kouzou() As String
        Get
            Return strKouzou
        End Get
        Set(ByVal value As String)
            strKouzou = value
        End Set
    End Property
#End Region

#Region "階層"
    ''' <summary>
    ''' 階層
    ''' </summary>
    ''' <remarks></remarks>
    Private strKaisou As String
    ''' <summary>
    ''' 階層
    ''' </summary>
    ''' <value></value>
    ''' <returns> 階層</returns>
    ''' <remarks></remarks>
    Public Property Kaisou() As String
        Get
            Return strKaisou
        End Get
        Set(ByVal value As String)
            strKaisou = value
        End Set
    End Property
#End Region

#Region "設計許容支持力"
    ''' <summary>
    ''' 設計許容支持力
    ''' </summary>
    ''' <remarks></remarks>
    Private deciSekkeiKyoyouSijiryoku As Decimal
    ''' <summary>
    ''' 設計許容支持力
    ''' </summary>
    ''' <value></value>
    ''' <returns> 設計許容支持力</returns>
    ''' <remarks></remarks>
    Public Property SekkeiKyoyouSijiryoku() As Decimal
        Get
            Return deciSekkeiKyoyouSijiryoku
        End Get
        Set(ByVal value As Decimal)
            deciSekkeiKyoyouSijiryoku = value
        End Set
    End Property
#End Region

#Region "根切り深さ"
    ''' <summary>
    ''' 根切り深さ
    ''' </summary>
    ''' <remarks></remarks>
    Private deciNegiriHukasa As Decimal
    ''' <summary>
    ''' 根切り深さ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 根切り深さ</returns>
    ''' <remarks></remarks>
    Public Property NegiriHukasa() As Decimal
        Get
            Return deciNegiriHukasa
        End Get
        Set(ByVal value As Decimal)
            deciNegiriHukasa = value
        End Set
    End Property
#End Region

#Region "予定基礎名"
    ''' <summary>
    ''' 予定基礎名
    ''' </summary>
    ''' <remarks></remarks>
    Private strYoteiKsMei As String
    ''' <summary>
    ''' 予定基礎名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 予定基礎名</returns>
    ''' <remarks></remarks>
    Public Property YoteiKsMei() As String
        Get
            Return strYoteiKsMei
        End Get
        Set(ByVal value As String)
            strYoteiKsMei = value
        End Set
    End Property
#End Region

#Region "車庫"
    ''' <summary>
    ''' 車庫
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyako As String
    ''' <summary>
    ''' 車庫
    ''' </summary>
    ''' <value></value>
    ''' <returns> 車庫</returns>
    ''' <remarks></remarks>
    Public Property Syako() As String
        Get
            Return strSyako
        End Get
        Set(ByVal value As String)
            strSyako = value
        End Set
    End Property
#End Region

#Region "予定盛土厚さ"
    ''' <summary>
    ''' 予定盛土厚さ
    ''' </summary>
    ''' <remarks></remarks>
    Private deciYoteiMoritutiAtusa As Decimal
    ''' <summary>
    ''' 予定盛土厚さ
    ''' </summary>
    ''' <value></value>
    ''' <returns> 予定盛土厚さ</returns>
    ''' <remarks></remarks>
    Public Property YoteiMoritutiAtusa() As Decimal
        Get
            Return deciYoteiMoritutiAtusa
        End Get
        Set(ByVal value As Decimal)
            deciYoteiMoritutiAtusa = value
        End Set
    End Property
#End Region

#Region "同時依頼棟数"
    ''' <summary>
    ''' 同時依頼棟数
    ''' </summary>
    ''' <remarks></remarks>
    Private intDoujiIraiTousuu As Integer
    ''' <summary>
    ''' 同時依頼棟数
    ''' </summary>
    ''' <value></value>
    ''' <returns> 同時依頼棟数</returns>
    ''' <remarks></remarks>
    Public Property DoujiIraiTousuu() As Integer
        Get
            Return intDoujiIraiTousuu
        End Get
        Set(ByVal value As Integer)
            intDoujiIraiTousuu = value
        End Set
    End Property
#End Region

#Region "建物用途名"
    ''' <summary>
    ''' 建物用途名
    ''' </summary>
    ''' <remarks></remarks>
    Private strTatemonoYoutoMei As String
    ''' <summary>
    ''' 建物用途名
    ''' </summary>
    ''' <value></value>
    ''' <returns> 建物用途名</returns>
    ''' <remarks></remarks>
    Public Property TatemonoYoutoMei() As String
        Get
            Return strTatemonoYoutoMei
        End Get
        Set(ByVal value As String)
            strTatemonoYoutoMei = value
        End Set
    End Property
#End Region

#Region "経由"
    ''' <summary>
    ''' 経由
    ''' </summary>
    ''' <remarks></remarks>
    Private intKeiyu As Integer
    ''' <summary>
    ''' 経由
    ''' </summary>
    ''' <value></value>
    ''' <returns> 経由</returns>
    ''' <remarks></remarks>
    Public Property Keiyu() As Integer
        Get
            Return intKeiyu
        End Get
        Set(ByVal value As Integer)
            intKeiyu = value
        End Set
    End Property
#End Region

#Region "オプション"
    ''' <summary>
    ''' オプション
    ''' </summary>
    ''' <remarks></remarks>
    Private strOptions As String = String.Empty
    ''' <summary>
    ''' オプション
    ''' </summary>
    ''' <value></value>
    ''' <returns>オプション</returns>
    ''' <remarks></remarks>
    Public Property Options() As String
        Get
            Return strOptions
        End Get
        Set(ByVal value As String)
            strOptions = value
        End Set
    End Property
#End Region

End Class